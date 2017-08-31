using AutoMapper;
using BusinessAppServer.Mapper;
using BusinessAppServer.ViewModel;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;


namespace BusinessAppServer.Manager
{
    public class ConfigManager : IConfigManager
    {

        private readonly IMapper _mapper;
        //  private readonly IHttpContextAccessor _contextAccessor;

        //public ConfigManager(IMapper mapper, IHttpContextAccessor contextAccessor)
        public ConfigManager(IMapper mapper)
        {
            _mapper = mapper;
            // _contextAccessor = contextAccessor;
        }
        //public static IMongoCollection<OrderDetails> Configration(string collection)
        //{
        //    const string connectionString = "mongodb://127.0.0.1:27017";
        //    // Create a MongoClient object by using the connection string
        //    return new MongoClient(connectionString).GetDatabase("StoreDB").GetCollection<OrderDetails>(collection);
        //}
        //public static List<OrderDetails> GetOrderDetails(IMongoCollection<OrderDetails> collection)
        //{
        //    return collection.Find(new BsonDocument()).ToList();
        //}

        public ResponseResult SubmitOrder(OrderDetailVm _orderDetails)
        {
            ResponseResult res = new ResponseResult();
            var _orderDetailDm = _mapper.Map<OrderDetailVm, OrderDetailDm>(_orderDetails);
            _orderDetailDm.orderPlacedOn = DateTime.Now;
            _orderDetailDm.orderId = Guid.NewGuid();
            IMongoCollection<OrderDetailDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<OrderDetailDm>("Orders");
            try
            {
                collection.InsertOne(_orderDetailDm);
                res.Status = "success";
                res.Message = "Order Submitted Successfully !!";
            }
            catch (Exception ex)
            {
                res.Status = "fail";
                res.Message = ex.Message;
            }
            return res;
        }

        public List<Filters> GetFilters()
        {
            IMongoCollection<Filters> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<Filters>("Category");
            return collection.Find(new BsonDocument()).ToList();
        }

        public List<Medicines> GetFiltersValues(string filterValues)
        {
            IMongoCollection<Medicines> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<Medicines>("Medicines");
            var result = collection.Find(f => f.filterId == filterValues);
            return result.ToList();

        }

        public List<Medicines> Search(string filterId, string searchItem, bool fromOrder)
        {
            IFindFluent<Medicines, Medicines> result = null;
            if (searchItem == null)
            {
                return null;
            }
            else
            {
                if (searchItem.ToString() == "undefined")
                {
                    return null;
                }
            }

            IMongoCollection<Medicines> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<Medicines>("Medicines");
            //var col =  collection.Find(new BsonDocument()).ToList().Find(f => f.filterId == filterId && f.medicineName.ToLower().StartsWith(searchItem.ToLower()));
            if (fromOrder)
            {
                result = collection.Find(f => f.filterId == filterId && f.medicineId == searchItem);
            }
            else
            {
                result = collection.Find(f => f.filterId == filterId && f.medicineName.ToLower().StartsWith(searchItem.ToLower()));
            }

            return result.ToList();
        }

        public List<OwnersDetails> FetchOwnersData()
        {
            List<OrderDetailVm> _orderDetailVM = new List<OrderDetailVm>();
            List<OwnersDetails> _ownerDetailVM = new List<OwnersDetails>();
            IMongoCollection<OrderDetailDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<OrderDetailDm>("Orders");
            var result = collection.Find(x => x.isActive == true).ToList();
            try
            {
                if (result != null && result.Count() > 0)
                {
                    _orderDetailVM = _mapper.Map<List<OrderDetailDm>, List<OrderDetailVm>>(result);
                    _ownerDetailVM = Mapping.OwnerDetailsData(_orderDetailVM);
                }
            }
            catch (Exception ex)
            {
            }
            return _ownerDetailVM;
        }

        public List<OwnersDetails> FetchUpdatedOwnersData(string orderId)
        {

            List<OrderDetailVm> _orderDetailVM = new List<OrderDetailVm>();
            List<OwnersDetails> _ownerDetailVM = new List<OwnersDetails>();
            IMongoCollection<OrderDetailDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<OrderDetailDm>("Orders");

            var filter = Builders<OrderDetailDm>.Filter.Eq("orderId", orderId);
            var update = Builders<OrderDetailDm>.Update
                .Set("isActive", false);
            var result = collection.UpdateOne(filter, update);
            return FetchOwnersData();
        }

        public List<Report> ReportGeneration(ReportVm reportVm)
        {
            var _reportDM = _mapper.Map<ReportVm, ReportDm>(reportVm);


            List<Report> __reportDM = new List<Report>();
            List<OrderDetailDm> _orderDetailDM = new List<OrderDetailDm>();
            OrderDetailDm _orderDetailDM1 = new OrderDetailDm();

            var collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<OrderDetailDm>("Orders");

            if (_reportDM.statusDm == "0")
            {
                _orderDetailDM = collection.Find(new BsonDocument()).ToList();
            }
            else if (_reportDM.statusDm == "1")
            {
                _orderDetailDM = collection.Find(d => d.isActive == true).ToList();
                // _orderDetailDM = collection.Find(x => x.isActive == true).ToList();
            }
            else
            {
                _orderDetailDM = collection.Find(x => x.isActive == false).ToList();
            }
            if (_orderDetailDM.Count > 0)
            {
                __reportDM = ReportGenerate(_orderDetailDM);
            }
            else
            {

            }

            return __reportDM;
        }

        public List<Report> ReportGenerate(List<OrderDetailDm> orderDetailDM)
        {
            List<Report> reportList = new List<Report>();
            foreach (OrderDetailDm item in orderDetailDM)
            {
                Report report = new Report()
                {
                    medicineName = item.medicineName,
                    medicineManufacturer = item.medicineManufacturer,
                    name = item.name,
                    phoneNumber = item.phoneNumber,
                    pinCode = item.pinCode,
                    street = item.street,
                    city = item.city,
                    state = item.state,
                    landmark = item.landmark,
                    verification = item.verification,
                    paymentOption = item.paymentOption,
                    isActive = item.isActive,
                    orderPlacedOn = item.orderPlacedOn,
                    orderCompleted = item.orderCompleted,

                };
                reportList.Add(report);
            }
            return reportList;
        }

        public LoginResponseVm Login(LoginRequestVm loginRequest)
        {
            LoginResponseDm response = new LoginResponseDm();
            IMongoCollection<LoginRequestDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<LoginRequestDm>("Login");
            var loginRequestDm = _mapper.Map<LoginRequestVm, LoginRequestDm>(loginRequest);
            var result = collection.Count(f => f.username == loginRequestDm.username && f.password == loginRequestDm.password);
            if (result == 1)
            {
                response.status = "OK";
                response.message = "Login Successfully";
                string isAuthenticate;
                const string sessionKey = "IsAuthenticated";

                var value = HttpHelper.HttpContext.Session.GetObjectFromJson<string>(sessionKey);
                if (string.IsNullOrEmpty(value))
                {
                    isAuthenticate = "hello lp";
                    HttpHelper.HttpContext.Session.SetObjectAsJson(sessionKey, isAuthenticate.ToString());
                    //var valude = HttpHelper.HttpContext.Session.GetString(sessionKey);
                    //System.WebHttpContext.Session.SetString(sessionKey, isAuthenticate.ToString());
                    var valude = HttpHelper.HttpContext.Session.GetString(sessionKey);
                }
                else
                {
                    //isAuthenticate = Convert.ToBoolean(value);
                    isAuthenticate = value;
                }
            }
            else
            {
                response.status = "Fail";
                response.message = "Login Fail";
            }
            var loginReponse = _mapper.Map<LoginResponseDm, LoginResponseVm>(response);
            return loginReponse;
        }

        public ResponseResult Registration(RegistrationVm registrationRequest)
        {
            ResponseResult res = new ResponseResult();
            LoginRequestDm reqLogin = new LoginRequestDm();
            IMongoCollection<RegistrationDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<RegistrationDm>("Register");
            IMongoCollection<LoginRequestDm> collectionLogin = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<LoginRequestDm>("Login");
            var registrationRequestDm = _mapper.Map<RegistrationVm, RegistrationDm>(registrationRequest);
            var emailExist = collection.Find(d => d.emailDm == registrationRequest.email.Trim());
            if (emailExist.Count() == 0)
            {
                try
                {
                    collection.InsertOne(registrationRequestDm);
                    reqLogin.password = registrationRequestDm.newPasswordDm.Trim();
                    reqLogin.username = registrationRequestDm.userNameDm.Trim();
                    reqLogin.email = registrationRequestDm.emailDm.Trim();
                    collectionLogin.InsertOne(reqLogin);

                    res.Status = "OK";
                    res.Message = "Registration done Successfully !!";
                }
                catch (Exception ex)
                {
                    res.Status = "fail";
                    res.Message = ex.Message;
                }
            }
            else
            {
                res.Status = "fail";
                res.Message = "Email Already Exists !!";
            }

            return res;
        }

        public ResponseResult ForgotPassword(string email)
        {
            ResponseResult res = new ResponseResult();
            //RegistrationDm reg = new RegistrationDm();
            //LoginRequestDm log = new LoginRequestDm();
            IMongoCollection<RegistrationDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<RegistrationDm>("Register");
            IMongoCollection<LoginRequestDm> logcollection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<LoginRequestDm>("Login");
            var emailExist = collection.Find(d => d.emailDm == email);
            if (emailExist.Count() == 1)
            {
                Random generator = new Random();
                String message = generator.Next(0, 1000000).ToString("D6");
                try
                {
                    //var emailMessage = new MimeMessage();

                    //emailMessage.From.Add(new MailboxAddress("Lovepreet", "preeet1000@gmail.com"));
                    //emailMessage.To.Add(new MailboxAddress("", email));
                    //emailMessage.Subject = "Reset Password !!";
                    //emailMessage.Body = new TextPart("plain") { Text = message };
                    var mimeMessage = new MimeMessage();
                    mimeMessage.From.Add(new MailboxAddress("Lovepreet", "lovepreet.com"));
                    mimeMessage.To.Add(new MailboxAddress("reset", email));
                    mimeMessage.Subject = "Reset Password !!";
                    mimeMessage.Body = new TextPart("plain") { Text = message };
                    string SmtpServer = "smtp.mail.yahoo.com";

                    int SmtpPortNumber = 587;
                    using (var client = new SmtpClient())
                    {
                        //client.LocalDomain = "some.domain.com";
                        //client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                        //client.SendAsync(emailMessage).ConfigureAwait(false);
                        //client.DisconnectAsync(true).ConfigureAwait(false);

                        // client.Connect("smtp.gmail.com", 465, false);
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                        client.Connect(SmtpServer, SmtpPortNumber, true);
                        // Note: only needed if the SMTP server requires authentication 
                        // Error 5.5.1 Authentication  
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate("", "");
                        client.Send(mimeMessage);
                        Console.WriteLine("The mail has been sent successfully !!");

                        client.Disconnect(true);
                    }
                    var filter = Builders<RegistrationDm>.Filter.Eq("emailDm", email);
                    var update = Builders<RegistrationDm>.Update
                        .Set("tempPassword", message)
                      .Set("newPasswordDm", message);
                    var result = collection.UpdateOne(filter, update);

                    var fil = Builders<LoginRequestDm>.Filter.Eq("email", email);
                    var updat = Builders<LoginRequestDm>.Update
                        .Set("password", message);
                    var reslt = logcollection.UpdateOne(fil, updat);



                    res.Status = "OK";
                    res.Message = "New Password Sent !!";
                }
                catch (Exception ex)
                {
                    res.Status = "fail";
                    res.Message = ex.Message;
                    res.Message = message;


                    //till smtp is not working        
                    var filter = Builders<RegistrationDm>.Filter.Eq("emailDm", email);
                    var update = Builders<RegistrationDm>.Update
                        .Set("tempPassword", message)
                      .Set("newPasswordDm", message);
                    var result = collection.UpdateOne(filter, update);

                    var fil = Builders<LoginRequestDm>.Filter.Eq("email", email);
                    var updat = Builders<LoginRequestDm>.Update
                        .Set("password", message);
                    var reslt = logcollection.UpdateOne(fil, updat);
                }
            }
            else
            {
                res.Status = "fail";
                res.Message = "Email is wrong !!";

            }
            return res;
        }

        public ResponseResult ResetPassword(ResetPasswordVm resetVm)
        {
            ResponseResult res = new ResponseResult();       
            IMongoCollection<RegistrationDm> collection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<RegistrationDm>("Register");
            IMongoCollection<LoginRequestDm> logcollection = new MongoClient("mongodb://127.0.0.1:27017").GetDatabase("StoreDB").GetCollection<LoginRequestDm>("Login");
            var resetDm = _mapper.Map<ResetPasswordVm, ResetPasswordDm>(resetVm);
            var dataExist = collection.Find(d => d.emailDm == resetDm.emailAddressDm.Trim() && d.newPasswordDm ==resetDm.oldPasswordDm.Trim());
            if (dataExist.Count() == 1)
            {               
                try
                {                   
                    var filter = Builders<RegistrationDm>.Filter.Eq("emailDm", resetDm.emailAddressDm);
                    var update = Builders<RegistrationDm>.Update
                        .Set("tempPassword", "")
                      .Set("newPasswordDm", resetDm.newPasswordDm);
                    var result = collection.UpdateOne(filter, update);

                    var fil = Builders<LoginRequestDm>.Filter.Eq("email", resetDm.emailAddressDm);
                    var updat = Builders<LoginRequestDm>.Update
                        .Set("password", resetDm.newPasswordDm);
                    var reslt = logcollection.UpdateOne(fil, updat);

                    res.Status = "OK";
                    res.Message = "Password Reset Succussfully !!";
                }
                catch (Exception ex)
                {
                    res.Status = "fail";
                    res.Message = ex.Message;                   
                }
            }
            else
            {
                res.Status = "fail";
                res.Message = "Email or old password is wrong!!";

            }
            return res;
        }
    }
}
