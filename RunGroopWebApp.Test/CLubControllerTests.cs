using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Services;
using RunGroopWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static System.Reflection.Metadata.BlobBuilder;

namespace RunGroopWebApp.Tests.Controller
{
    public class ClubControllerTests
    {
        private ClubController _clubController;
        private IClubRepository _clubRepository;
        private IPhotoService _photoService;
        private IHttpContextAccessor _httpContextAccessor;
        public ClubControllerTests()
        {
            //Dependencies
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT
            _clubController = new ClubController(_clubRepository, _photoService, _httpContextAccessor);
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //Arrange - What do i need to bring in?
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);
            //Act
            var result = _clubController.Index();
            //Assert - Object check actions
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public async Task Index_ModelInViewResult_ShouldNotBeNull()
        {
            // Arrange
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(Task.FromResult(clubs.AsEnumerable()));

            // Act
            var result = await _clubController.Index();

            // Assert
            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult.Model.Should().NotBeNull();
        }



        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            //Arrange
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(club);
            //Act
            var result = _clubController.Detail(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public async Task ClubController_Detail_ShouldReturnViewResult()
        {
            // Arrange
            int id = 1;
            var club = A.Fake<Club>();

            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(Task.FromResult(club));

            // Act
            var result = await _clubController.Detail(id);

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
        [Fact]
        public async Task Detail_ModelInViewResult_ShouldNotBeNull()
        {
            // Arrange
            int id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(Task.FromResult(club));

            // Act
            var result = await _clubController.Detail(id);

            // Assert
            var viewResult = result as ViewResult;
            viewResult.Should().NotBeNull();
            viewResult.Model.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_GET_ReturnsViewResult()
        {
            // Act
            var result = _clubController.Create();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }






    }
}