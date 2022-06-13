using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Timelogger.Api.Models.TimeRegistration;
using Timelogger.Api.Services.TimeRegistration;
using Timelogger.Api.Services.TimeRegistration.cs;
using Timelogger.Persistence.Contracts;

namespace UnitTest.TimeLogger
{
    [TestClass]
    public class UnitTest1
    {

        private readonly Mock<ITimeRegistrationRepository> _timeRegisrtationRepo = new Mock<ITimeRegistrationRepository>();
        private readonly Mock<IProjectRepository> _projectRepo = new Mock<IProjectRepository>();
        private ITimeRegistrationService _service;

        [TestInitialize]
        public void Initializa()
        {
            _service = new TimeRegistrationService(_timeRegisrtationRepo.Object, _projectRepo.Object);
        }

        [TestMethod]
        public void CreateValidInput()
        {
            try
            {
                //arrange
                var guid = System.Guid.NewGuid();
                var project = GetProject(guid);
                _projectRepo.Setup(s => s.GetSingle(guid)).Returns(project);
                var registration = new CreateTimeRegistration()
                {
                    Date = System.DateTime.Now,
                    Minutes = 40,
                    ProjectId = guid
                };
                _timeRegisrtationRepo.Setup(s => s.Create()).

                //act
                _service.Create(registration);

                //assert
                Assert.IsTrue(true);
            }
            catch (System.Exception ex)
            {
                Assert.IsTrue(false);
            }
        }


        [TestMethod]
        public void CreateInvalidDateInput()
        {
            //arrange
            var guid = System.Guid.NewGuid();
            var project = GetProject(guid);
            _projectRepo.Setup(s => s.GetSingle(guid)).Returns(project);
            var registration = new CreateTimeRegistration()
            {
                Date = System.DateTime.Now.AddDays(10),
                Minutes = 40,
                ProjectId = guid
            };

            //act
            Assert.ThrowsException<ArgumentException>(() => _service.Create(registration));
        }

        private Timelogger.Entities.Project GetProject(System.Guid id)
        {
            var project = new Timelogger.Entities.Project()
            {
                Id = id,
                CompanyId = System.Guid.Empty,
                Deadline = System.DateTime.Now.AddDays(5),
                Start = System.DateTime.Now,
                HourlyRate = 100,
                Name = "mock",
                TimeRegistrations = new List<Timelogger.Entities.TimeRegistration>()
            };

            return project;
        }
    }
}
