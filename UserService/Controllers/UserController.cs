using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
using UserService.DataServices;
using UserService.Dtos;
using UserService.Objects;

namespace UserService.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;
        private readonly IUserServiceClient _userServiceClient;
        private readonly IValidator<UserCreateDto> _validator;

        public UserController(IUserRepo repository
            , IMapper mapper
            , IUserServiceClient userServiceClient
            , IValidator<UserCreateDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _userServiceClient = userServiceClient;
            _validator = validator;
        }


        [Route("{id}", Name = "GetUserById")]
        [HttpGet]
        public ActionResult GetUserById(int id)
        {
            try
            {
                _userServiceClient.PublishX("test xoxoxoxo");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }
            return Ok(_mapper.Map<UserReadDto>(_repository.Details(id)));
        }

        [HttpPost]
        [Route("")]
        public ActionResult Create(UserCreateDto userCreatedDto)
        {
            var userCreatedObject = _mapper.Map<User>(userCreatedDto);

            _repository.Create(userCreatedObject);
            _repository.Save();

            var userReadDto = _mapper.Map<UserReadDto>(userCreatedObject);

            return CreatedAtRoute(nameof(GetUserById), new { Id = userReadDto.Id }, userReadDto);
        }


        [HttpPut]
        [Route("{Id}/")]
        public ActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var user = _mapper.Map<User>(userUpdateDto);
            user.Id = id;

            _repository.Update(user);
            _repository.Save();

            return Ok(user);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteUser(int id)
        {
            _repository.Delete(id);
            _repository.Save();
            return Ok();
        }

        [HttpGet]
        [Route("Health")]
        public ActionResult Health()
        {
            return Ok("--> Healthy");
        }
    }
}