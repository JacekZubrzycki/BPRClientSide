using BPRMobileApp.Models;
using BPRMobileApp.Models.Requests;
using BPRMobileApp.Services;

namespace XUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task RegisterUser_Test()
        {
            //Arrange

            UserRegisterDTO teacherUser = new UserRegisterDTO("teacher20", "unittest@test.com", "unit", "test", "unittest", "unit", "11111", "UnitTest0£");
            UserRegisterDTO studentUser = new UserRegisterDTO("student1234", "unittest@test.com", "unit", "test", "unittest", "unit", "11111", "UnitTest0£");
            bool teacherAccount = true;
            bool stuudentAccount = false;
            MobileDataProvider provider = new MobileDataProvider();

            //Act

            HttpResponseMessage teacherResponse = await provider.RegisterUser(teacherUser, teacherAccount);
            HttpResponseMessage studentResponse = await provider.RegisterUser(studentUser, stuudentAccount);

            //Assert

            //Test passed; only one user with the same username can be in the database; now method should return 500 HTTP respond message
            //Assert.True(teacherResponse.IsSuccessStatusCode);
            //Assert.True(studentResponse.IsSuccessStatusCode);

            Assert.False(teacherResponse.IsSuccessStatusCode);
            Assert.False(studentResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task LoginUser_Test()
        {
            //Arrange

            UserLoginDTO user = new UserLoginDTO("teacher20", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);

            //Assert

            Assert.True(loginRespond.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AddSubject_Test()
        {
            //Arrange

            int subject_Id = 10;
            Token token;
            UserLoginDTO user = new UserLoginDTO("teacher20", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage addSubjectRespond = await provider.AddSubject(subject_Id, token.token);

            //Assert

            Assert.True(addSubjectRespond.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetSubjects_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("teacher20", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage getSubjectsRespond = await provider.GetSubjects(token.token);

            //Assert

            Assert.True(getSubjectsRespond.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetPostedSubjects_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("student1234", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage getPosedSubjectsResponse = await provider.GetPostedSubjects(token.token);

            //Assert

            Assert.True(getPosedSubjectsResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task PostAnOffer_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("teacher20", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            OfferDTORequest offer = new OfferDTORequest(8, 20, 30, from, to);

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage postAnOfferResponse = await provider.PostAnOffer(token.token, offer);

            //Assert

            Assert.True(postAnOfferResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetOfferWithSubject_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("teacher20", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();
            string subject = "Math";

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage getOfferWithSubjectResponse = await provider.GetOffersWithSubject(token.token, subject);
        
            //Assert

            Assert.True(getOfferWithSubjectResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetBookedTimesByOfferId_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("student1234", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();
            int offerId = 4;

            //Act


            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage getBookedTimesByOfferIdResponse = await provider.GetBookedTiemsByOfferId(token.token, offerId);

            //Assert

            Assert.True(getBookedTimesByOfferIdResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task BookAnOffer_Test()
        {
            //Arrange

            Token token;
            UserLoginDTO user = new UserLoginDTO("student1234", "UnitTest0£");
            MobileDataProvider provider = new MobileDataProvider();
            MobileSerializer serializer = new MobileSerializer();
            DateTime from = new DateTime();
            DateTime to = new DateTime();
            BookTimeDTO offer = new BookTimeDTO(3, from, to);

            //Act

            HttpResponseMessage loginRespond = await provider.LoginUser(user);
            token = await serializer.DeserialzieToToken(loginRespond);
            HttpResponseMessage bookAnOfferResponse = await provider.BookAnOffer(token.token, offer);

            //Assert

            Assert.True(bookAnOfferResponse.IsSuccessStatusCode);
        }

    }
}