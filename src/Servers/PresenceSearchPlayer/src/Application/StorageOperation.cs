using System.Collections.Generic;
using System.Linq;
using UniSpy.Server.PresenceSearchPlayer.Abstraction.Interface;
using UniSpy.Server.PresenceSearchPlayer.Contract.Result;
using UniSpy.Server.Core.Database.DatabaseModel;

namespace UniSpy.Server.PresenceSearchPlayer.Application
{
    internal sealed class StorageOperation : IStorageOperation
    {
        public static IStorageOperation Persistance = new StorageOperation();
        public bool VerifyEmail(string email)
        {
            using (var db = new UniSpyContext())
            {
                return db.Users.Where(e => e.Email == email).Count() < 1;
            }
        }
        public bool VerifyEmailAndPassword(string email, string password)
        {
            using (var db = new UniSpyContext())
            {
                return db.Users.Where(u => u.Email == email && u.Password == password).Count() < 1;
            }
        }
        public int? GetProfileId(string email, string password, string nickName, int? partnerId)
        {
            using (var db = new UniSpyContext())
            {
                // Not every game uses PartnerId; optional
                var result = from p in db.Profiles
                             join u in db.Users on p.Userid equals u.Userid
                             join sp in db.Subprofiles on p.Profileid equals sp.Profileid
                             where u.Email.Equals(email)
                             && u.Password.Equals(password)
                             && p.Nick.Equals(nickName)
                             || sp.Partnerid.Equals(partnerId)
                             select p.Profileid;

                if (result.Count() == 1)
                {
                    return result.First();
                }
                else
                {
                    return null;
                }
            }
        }

        public void AddSubProfile(Subprofile subProfile)
        {
            using (var db = new UniSpyContext())
            {
                db.Subprofiles.Add(subProfile);
                db.SaveChanges();
            }
        }

        public void AddProfile(Profile profile)
        {
            using (var db = new UniSpyContext())
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
            }
        }
        public void AddUser(User user)
        {
            using (var db = new UniSpyContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
        public void UpdateProfile(Profile profile)
        {
            using (var db = new UniSpyContext())
            {
                db.Profiles.Update(profile);
                db.SaveChanges();
            }
        }
        public void UpdateSubProfile(Subprofile subprofile)
        {
            using (var db = new UniSpyContext())
            {
                db.Subprofiles.Update(subprofile);
                db.SaveChanges();
            }
        }

        public User GetUser(string email)
        {
            using (var db = new UniSpyContext())
            {
                return db.Users.Where(u => u.Email == email).Select(u => u).FirstOrDefault();
            }
        }

        public Profile GetProfile(int userId, string nickName)
        {
            using (var db = new UniSpyContext())
            {
                return db.Profiles.Where(p => p.Userid == userId && p.Nick == nickName).FirstOrDefault();
            }
        }

        public Subprofile GetSubProfile(int profileId, int namespaceId, int productId)
        {
            using (var db = new UniSpyContext())
            {
                return db.Subprofiles.Where(s =>
                        s.Profileid == profileId &&
                        s.Namespaceid == namespaceId &&
                        s.Productid == productId).FirstOrDefault();
            }
        }

        public List<NicksDataModel> GetAllNickAndUniqueNick(string email, string password, int namespaceId)
        {
            using (var db = new UniSpyContext())
            {
                var result = from u in db.Users
                             join p in db.Profiles on u.Userid equals p.Userid
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             where u.Email == email
                             && u.Password == password
                             && n.Namespaceid == namespaceId
                             select new NicksDataModel
                             {
                                 NickName = p.Nick,
                                 UniqueNick = n.Uniquenick
                             };
                return result.ToList();
            }
        }

        public List<OthersDatabaseModel> GetFriendsInfo(int profileId, int namespaceId, string gameName)
        {
            var dataList = new List<OthersDatabaseModel>();
            using (var db = new UniSpyContext())
            {
                var result = from b in db.Friends
                             where b.Profileid == profileId && b.Namespaceid == namespaceId
                             select b.Targetid;

                foreach (var info in result)
                {
                    var b = from p in db.Profiles
                            join n in db.Subprofiles on p.Profileid equals n.Profileid
                            join u in db.Users on p.Userid equals u.Userid
                            where n.Namespaceid == namespaceId
                            && n.Profileid == info && n.Gamename == gameName
                            select new OthersDatabaseModel
                            {
                                ProfileId = p.Profileid,
                                Nick = p.Nick,
                                Uniquenick = n.Uniquenick,
                                Lastname = p.Lastname,
                                Firstname = p.Firstname,
                                Userid = u.Userid,
                                Email = u.Email
                            };

                    dataList.Add(b.First());
                }
            }
            return dataList;
        }

        public List<OthersListDatabaseModel> GetMatchedProfileIdInfos(List<int> profileIds, int namespaceId)
        {
            var dataList = new List<OthersListDatabaseModel>();
            using (var db = new UniSpyContext())
            {
                foreach (var pid in profileIds)
                {
                    var result = from n in db.Subprofiles
                                 where n.Profileid == pid
                                 && n.Namespaceid == namespaceId
                                 //select new { uniquenick = n.Uniquenick };
                                 select new OthersListDatabaseModel
                                 {
                                     ProfileId = n.Profileid,
                                     Uniquenick = n.Uniquenick
                                 };

                    dataList.Add(result.First());
                }
            }
            return dataList;
        }

        public List<SearchDataBaseModel> GetMatchedInfosByNick(string nickName)
        {
            using (var db = new UniSpyContext())
            {
                var result = from p in db.Profiles
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             join u in db.Users on p.Userid equals u.Userid
                             where p.Nick == nickName
                             //&& n.Namespaceid == _request.NamespaceID
                             select new SearchDataBaseModel
                             {
                                 ProfileId = n.Profileid,
                                 Nick = p.Nick,
                                 Uniquenick = n.Uniquenick,
                                 Email = u.Email,
                                 Firstname = p.Firstname,
                                 Lastname = p.Lastname,
                                 NamespaceID = n.Namespaceid
                             };
                return result.ToList();
            }

        }

        public List<SearchDataBaseModel> GetMatchedInfosByEmail(string email)
        {

            using (var db = new UniSpyContext())

            {
                var result = from p in db.Profiles
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             join u in db.Users on p.Userid equals u.Userid
                             where u.Email == email
                             select new SearchDataBaseModel
                             {
                                 ProfileId = n.Profileid,
                                 Nick = p.Nick,
                                 Uniquenick = n.Uniquenick,
                                 Email = u.Email,
                                 Firstname = p.Firstname,
                                 Lastname = p.Lastname,
                                 NamespaceID = n.Namespaceid
                             };
                return result.ToList();
            }

        }

        public List<SearchDataBaseModel> GetMatchedInfosByNickAndEmail(string nickName, string email)
        {
            using (var db = new UniSpyContext())
            {
                var result = from p in db.Profiles
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             join u in db.Users on p.Userid equals u.Userid
                             where p.Nick == nickName && u.Email == email
                             //&& n.Namespaceid == _request.NamespaceID
                             //&& n.Gamename == _request.GameName
                             //&& n.Partnerid == _request.PartnerID
                             select new SearchDataBaseModel
                             {
                                 ProfileId = n.Profileid,
                                 Nick = p.Nick,
                                 Uniquenick = n.Uniquenick,
                                 Email = u.Email,
                                 Firstname = p.Firstname,
                                 Lastname = p.Lastname,
                                 NamespaceID = n.Namespaceid
                             };
                return result.ToList();
            }
        }

        public List<SearchDataBaseModel> GetMatchedInfosByUniqueNickAndNamespaceId(string uniqueNick, int namespaceId)
        {

            using (var db = new UniSpyContext())
            {
                var result = from p in db.Profiles
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             join u in db.Users on p.Userid equals u.Userid
                             where n.Uniquenick == uniqueNick
                             && n.Namespaceid == namespaceId
                             //&& n.Gamename == _request.GameName
                             //&& n.Partnerid == _request.PartnerID
                             select new SearchDataBaseModel
                             {
                                 ProfileId = n.Profileid,
                                 Nick = p.Nick,
                                 Uniquenick = n.Uniquenick,
                                 Email = u.Email,
                                 Firstname = p.Firstname,
                                 Lastname = p.Lastname,
                                 NamespaceID = n.Namespaceid
                             };
                return result.ToList();
            }

        }

        public List<SearchUniqueDatabaseModel> GetMatchedInfosByNamespaceId(List<int> namespaceIds, string uniqueNick)
        {
            using (var db = new UniSpyContext())
            {
                var dataList = new List<SearchUniqueDatabaseModel>();
                foreach (var nsId in namespaceIds)
                {
                    var result = from p in db.Profiles
                                 join n in db.Subprofiles on p.Profileid equals n.Profileid
                                 join u in db.Users on p.Userid equals u.Userid
                                 where n.Uniquenick == uniqueNick
                                 && n.Namespaceid == nsId
                                 select new SearchUniqueDatabaseModel
                                 {
                                     ProfileId = n.Profileid,
                                     Nick = p.Nick,
                                     Uniquenick = n.Uniquenick,
                                     Email = u.Email,
                                     Firstname = p.Firstname,
                                     Lastname = p.Lastname,
                                     NamespaceID = n.Namespaceid
                                 };
                    dataList.Add(result.First());
                }
                return dataList;
            }
        }

        public bool IsUniqueNickExist(string uniqueNick, int namespaceId, string gameName)
        {
            using (var db = new UniSpyContext())
            {
                var result = from p in db.Profiles
                             join n in db.Subprofiles on p.Profileid equals n.Profileid
                             where n.Uniquenick == uniqueNick
                             && n.Namespaceid == namespaceId
                             && n.Gamename == gameName
                             select p.Profileid;

                if (result.Count() != 0)
                {
                    return true;
                }

                return false;
            }
        }
        public bool IsEmailExist(string email)
        {
            using (var db = new UniSpyContext())
            {
                var result = from u in db.Users
                                 //According to FSW partnerid is not nessesary
                             where u.Email == email
                             select u.Userid;

                if (result.Count() == 0)
                {
                    return false;
                }
                return true;
            }
        }
    }
}