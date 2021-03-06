﻿using System;
using System.Collections.Generic;
using CombinedAPI.Entities;
using CombinedAPI.neo4j;
using CombinedAPI.redis;

namespace CombinedAPI
{
    #region Enums
    /// <summary>
    /// Registration outcome.
    /// </summary>
    public enum RegistrationOutcome
    {  
        SUCCESS,
        USERNAME_IN_USE,
        FALIURE
    }

    /// <summary>
    /// Redis sets.
    /// </summary>
    public enum RedisSets
    {
        users,
        active_users,
        locations
    }
    #endregion

    public class DataAPI
    {
        #region Singleton
        private static DataAPI _instance = new DataAPI();
        public static DataAPI Instance { get => _instance; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CombinedAPI.DataAPI"/> class.
        /// </summary>
        private DataAPI()
        {
        }
        #endregion

        #region Methodes
        /// <summary>
        /// Checks the username.
        /// </summary>
        /// <returns><c>true</c>, if username was checked, <c>false</c> otherwise.</returns>
        /// <param name="username">Username.</param>
        bool CheckUsername(string username)
        {
            return !RedisManager.Instance.SSetIsInSet(RedisSets.users.ToString(),
                                                                username);
        }

        /// <summary>
        /// Register the specified profil.
        /// </summary>
        /// <returns>The register.</returns>
        /// <param name="profil">Profil.</param>
        public RegistrationOutcome Register(Profil profil)
        {
            try
            {
                if (CheckUsername(profil.KorisnickoIme))
                {
                    Neo4jManager.Instance.GenerateNewNode(profil);
                    RedisManager.Instance.SSetValue(RedisSets.users.ToString(),
                                                    profil.KorisnickoIme);
                    return RegistrationOutcome.SUCCESS;
                }
                else
                {
                    return RegistrationOutcome.USERNAME_IN_USE;
                }
            }
            catch (Exception exc)
            {
                return RegistrationOutcome.FALIURE;
            }
        }

        /// <summary>
        /// Login the specified profil.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="profil">Profil.</param>
        private bool LoginRedis(Profil profil)
        {
            try
            {
                RedisManager.Instance.SSetValue(RedisSets.active_users.ToString(),
                                                profil.KorisnickoIme 
                                                + " " 
                                                + profil.Ime
                                                + " "
                                                + profil.Prezime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Login the specified username and password. It will return null if
        /// login was not successfull from any reason.
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public Profil Login(string username, string password)
        {
            Profil profil = new Profil();
            profil.IdentificatorName = "KorisnickoIme";
            profil.IdentificatorValue = username;

            try
            {
                List<Profil> profili = this.GetEntity<Profil>(profil);

                if (profili[0].Lozinka == password)
                {
                    profil = profili[0];
                    if (LoginRedis(profil))
                        return profil;
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Logout the specified profil.
        /// </summary>
        /// <returns>The logout.</returns>
        /// <param name="profil">Profil.</param>
        public bool Logout(Profil profil)
        {
            try
            {
                RedisManager.Instance.SRemoveValue(RedisSets.active_users.ToString(),
                                                   profil.KorisnickoIme 
                                                   + " " 
                                                   + profil.Ime
                                                   + " "
                                                   + profil.Prezime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all users usernames.
        /// </summary>
        /// <returns>The all users usernames.</returns>
        public List<string> GetAllUsersUsernames()
        {
            try
            {
                return RedisManager.Instance.GetAllFromSet(RedisSets.users.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all active users usernames.
        /// </summary>
        /// <returns>The all active users usernames.</returns>
        public List<string> GetAllActiveUserDetails()
        {
            try
            {
                return RedisManager.Instance.GetAllFromSet(RedisSets.active_users.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Create new location Node if one don't already exists.
        /// </summary>
        /// <param name="lokacija">Location object.</param>
        /// <returns>Function returns true if the execution was without exceptions.
        /// Otherwise false.
        /// </returns>
        public bool CreateLocationIfNeeded(Lokacija lokacija)
        {
            try
            {
                bool exists = RedisManager.Instance.SSetIsInSet(RedisSets.locations.ToString(),
                    lokacija.Naziv);
                if(!exists)
                {
                    RedisManager.Instance.SSetIsInSet(RedisSets.locations.ToString(),
                        lokacija.Naziv);
                    Neo4jManager.Instance.GenerateNewNode(lokacija);
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>The entity.</returns>
        /// <param name="tObject">T object is Node object which can be empty but 
        /// IdentifierName and IdentifierValue must be set.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public List<T> GetEntity<T>(object tObject)
        {
            try
            {
                string query = CypherCodeGenerator.Instance.GenerateGetNodeQuery(tObject);
                return Neo4jManager.Instance.ExecuteMatchQuery<T>(query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates the entity. Entity can be of type Slika, Profil etc.
        /// </summary>
        /// <returns><c>true</c>, if entity was created, <c>false</c> otherwise.</returns>
        /// <param name="tObject">T object.</param>
        public bool CreateEntity(Node tObject)
        {
            try
            {
                Neo4jManager.Instance.GenerateNewNode(tObject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    
        /// <summary>
        /// Edits the entity. Entity can be of type Slika, Profil etc.
        /// </summary>
        /// <returns><c>true</c>, if entity was edited, <c>false</c> otherwise.</returns>
        /// <param name="tObject">T object.</param>
        public bool EditEntity(Node tObject)
        {
            try
            {
                Neo4jManager.Instance.EditNode(tObject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the entity. Entity can be of type Slika, Profil etc.
        /// </summary>
        /// <returns><c>true</c>, if entity was deleted, <c>false</c> otherwise.</returns>
        /// <param name="tObject">T object.</param>
        public bool DeleteEntity(Node tObject)
        {
            try
            {
                Neo4jManager.Instance.DeleteNode(tObject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the relationship. Relationships can be of type Lajk, Tag etc.
        /// </summary>
        /// <returns><c>true</c>, if relationship was created, <c>false</c> otherwise.</returns>
        /// <param name="tObject">T object.</param>
        public bool CreateRelationship(Relationship tObject)
        {
            try
            {
                Neo4jManager.Instance.GenerateNewRelation(tObject);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes the relationship. Relationships can be of type Lajk, Tag etc.
        /// </summary>
        /// <returns><c>true</c>, if relationship was deleted, <c>false</c> otherwise.</returns>
        /// <param name="tObject">T object.</param>
        public bool DeleteRelationship(Relationship tObject)
        {
            try
            {
                Neo4jManager.Instance.DeleteRelationship(tObject);

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets all pictures for profile.
        /// </summary>
        /// <returns>The all pictures for profile.</returns>
        /// <param name="profil">Profil.</param>
        public List<Slika> GetAllPicturesForProfile(Profil profil)
        {
            string query = "MATCH(e: Slika) " +
                           "WHERE e.Username = '" + profil.KorisnickoIme + "' " +
                           "RETURN e;";
            try
            {
                return Neo4jManager.Instance.ExecuteMatchQuery<Slika>(query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all likes for photo.
        /// </summary>
        /// <returns>The all likes for photo.</returns>
        /// <param name="slika">Slika.</param>
        public List<Profil> GetAllLikesForPhoto(Slika slika)
        {
            string query = "match(a: Profil) -[r: Lajk]->(b: Slika { Kljuc: '" + slika.Kljuc + "'}) return a";

            try
            {
                return Neo4jManager.Instance.ExecuteMatchQuery<Profil>(query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets all nodes that match auto generated query which is generated from relObj.
        /// Both objects in relObj must have IdentificatorName and IdentificatorValue set.
        /// If UseInWhereClause is set to true for Relationships Node object it will be used
        /// in creating query. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relObj"></param>
        /// <param name="resultNodeSide"></param>
        /// <returns></returns>
        public List<T> GetNodesFromRelation<T>(Relationship relObj, ResultNodeSide resultNodeSide)
        {
            string query = CypherCodeGenerator.Instance.GenerateGetNodeFromRelationCypherQuery(relObj, resultNodeSide);

            try
            {
                Type type = relObj.FirstObject.GetType();
                return Neo4jManager.Instance.ExecuteMatchQuery<T>(query);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// Deletes the evrything from both Redis and Neo4j database. Carefull!
        /// </summary>
        public void DeleteEvrything()
        {
            Neo4jManager.Instance.DeleteEverything();
            RedisManager.Instance.DeleteEverything();
        }
        #endregion
    }
}
