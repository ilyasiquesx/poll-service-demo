using System;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.Commands.Topics;
using Service.Commands.Votes;
using Service.Queries.Topics;

namespace WebApplication.Controllers
{
    [Authorize]
    public class TopicController : BaseController
    {
        private readonly ILogger<TopicController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public TopicController(ILogger<TopicController> logger, IMediator mediator, UserManager<User> userManager)
        {
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var result = await _mediator.Send(new GetAllTopicsQuery());
                return View(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Message}", "Failed at getting Index page");
                return ErrorView(e.Message, HttpContext.TraceIdentifier);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Message}", "Failed at getting Create page");
                return ErrorView(e.Message, HttpContext.TraceIdentifier);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicCommand command)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Wrong input parameters");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            
            try
            {
                command.UserId = user.Id;
                var result = await _mediator.Send(command);
                return RedirectToAction("Details", new {id = result});
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Message} {TopicTitle} {UserId} {OptionsCount}",
                    "Failed at creating topic action", command.Title, command.UserId, command.AddOptionModels.Count);
                return ErrorView(e.Message, HttpContext.TraceIdentifier);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            try
            {
                var result = await _mediator.Send(new GetTopicByIdQuery
                {
                    UserId = user.Id,
                    Id = id
                });
                return View(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Message} {UserId} {TopicId}", "Failed at getting topic details", user.Id, id);
                return ErrorView(e.Message, HttpContext.TraceIdentifier);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Vote(AddVoteCommand command)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            try
            {
                command.UserId = user.Id;
                await _mediator.Send(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Message} {UserId} {TopicId} {OptionId}",
                "Failed at creating vote for topic", user.Id, command.TopicId, command.OptionId);
                return ErrorView(e.Message, HttpContext.TraceIdentifier);
            }

            return RedirectToAction("Details", new {id = command.TopicId});
        }
    }
}