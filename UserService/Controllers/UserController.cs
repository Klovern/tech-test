using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserService.Data;
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

        public UserController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [Route("{id}", Name = "GetUserById")]
        [HttpGet]
        public ActionResult GetUserById(int id)
        {
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