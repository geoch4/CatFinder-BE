using APILayer.Controllers;
using ApplicationLayer.Comments.Commands.CreateComment;
using ApplicationLayer.Comments.Commands.DeleteComment;
using ApplicationLayer.Comments.DTOs;
using ApplicationLayer.Comments.Queries.GetAllComments;
using DomainLayer.Models.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestLayer.Controllers;

// Tests for CommentsController — covers fetching, posting, and deleting comments on advertisements.
[TestFixture]
public class CommentsControllerTests
{
    private Mock<ISender> _mediator;       // fake version of MediatR — lets us control what it returns
    private CommentsController _controller; // the real controller we are testing

    // Runs before every test — creates fresh fakes and wires them into the controller.
    [SetUp]
    public void SetUp()
    {
        _mediator = new Mock<ISender>();                          // create a blank fake mediator
        _controller = new CommentsController(_mediator.Object);  // inject the fake into the controller
    }

    // All comments for a given advertisement are fetched → controller should always return 200 OK (even if empty).
    [Test]
    public async Task GetByAdvertisement_ReturnsOk_Always()
    {
        // Arrange
        _mediator                                                              // tell the fake mediator:
            .Setup(m => m.Send(                                               //   when Send() is called
                It.IsAny<GetCommentsByAdvertisementQuery>(),                  //   with any GetCommentsByAdvertisementQuery
                It.IsAny<CancellationToken>()))                               //   and any cancellation token
            .ReturnsAsync(                                                    //   then return asynchronously
                OperationResult<List<CommentResponseDto>>.Success([]));       //   a successful result with an empty list

        // Act
        var result = await _controller.GetByAdvertisement(1); // call GetByAdvertisement with advertisement ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>()); // result must be a 200 OK
    }

    // A new comment is posted successfully → controller should return 201 Created.
    [Test]
    public async Task Create_Returns201_WhenSuccess()
    {
        // Arrange
        _mediator                                                    // tell the fake mediator:
            .Setup(m => m.Send(                                      //   when Send() is called
                It.IsAny<CreateCommentCommand>(),                    //   with any CreateCommentCommand
                It.IsAny<CancellationToken>()))                      //   and any cancellation token
            .ReturnsAsync(                                           //   then return asynchronously
                OperationResult<CommentResponseDto>.Success(         //   a successful result
                    new CommentResponseDto()));                       //   with an empty comment response

        // Act
        var result = await _controller.Create(                       // call Create with:
            1,                                                       //   advertisement ID = 1
            new CreateCommentDto { Body = "Hello" });                //   a dto with a body (Body is required)

        // Assert
        Assert.That(
            (result as ObjectResult)!.StatusCode,        // cast the result to ObjectResult and read the status code
            Is.EqualTo(StatusCodes.Status201Created));   // it must be 201
    }

    // Posting a comment fails (e.g. the advertisement does not exist) → controller should return 400 Bad Request.
    [Test]
    public async Task Create_ReturnsBadRequest_WhenFailure()
    {
        // Arrange
        _mediator                                                    // tell the fake mediator:
            .Setup(m => m.Send(                                      //   when Send() is called
                It.IsAny<CreateCommentCommand>(),                    //   with any CreateCommentCommand
                It.IsAny<CancellationToken>()))                      //   and any cancellation token
            .ReturnsAsync(                                           //   then return asynchronously
                OperationResult<CommentResponseDto>.Failure(         //   a failed result
                    "Advertisement not found."));                    //   with this error message

        // Act
        var result = await _controller.Create(                       // call Create with:
            99,                                                      //   an advertisement ID that does not exist
            new CreateCommentDto { Body = "Hello" });                //   a valid dto body

        // Assert
        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>()); // result must be a 400 Bad Request
    }

    // A comment is deleted successfully → controller should return 204 No Content.
    [Test]
    public async Task Delete_ReturnsNoContent_WhenSuccess()
    {
        // Arrange
        _mediator                                   // tell the fake mediator:
            .Setup(m => m.Send(                    //   when Send() is called
                It.IsAny<DeleteCommentCommand>(),  //   with any DeleteCommentCommand
                It.IsAny<CancellationToken>()))    //   and any cancellation token
            .ReturnsAsync(                         //   then return asynchronously
                OperationResult<bool>.Success(true)); //   a successful result

        // Act
        var result = await _controller.Delete(1); // call Delete with ID = 1

        // Assert
        Assert.That(result, Is.InstanceOf<NoContentResult>()); // result must be a 204 No Content
    }

    // Delete is attempted but the comment does not exist → controller should return 404 Not Found.
    [Test]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        _mediator                                   // tell the fake mediator:
            .Setup(m => m.Send(                    //   when Send() is called
                It.IsAny<DeleteCommentCommand>(),  //   with any DeleteCommentCommand
                It.IsAny<CancellationToken>()))    //   and any cancellation token
            .ReturnsAsync(                         //   then return asynchronously
                OperationResult<bool>.Failure(     //   a failed result
                    "Comment not found."));        //   with this error message

        // Act
        var result = await _controller.Delete(99); // call Delete with an ID that does not exist

        // Assert
        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>()); // result must be a 404 Not Found
    }
}
