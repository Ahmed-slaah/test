using DemoProject.Core.DTOs;
using DemoProject.Core.IHelper;
using DemoProject.Core.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;

namespace DemoProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public static int roleId;
        public static string roleName;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController( IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddUserDTO addUserDTO = new AddUserDTO();
            addUserDTO.roles = new List<RolesDto>();
            var Data = await _unitOfWork.Roles.GetAll();
            foreach(var role in Data)
            {
                RolesDto rolesDto = new RolesDto();
                rolesDto.Name = role.Name;
                rolesDto.Id = role.Id;
                addUserDTO.roles.Add(rolesDto);
            } 
            return View(addUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddUserDTO addUserDTO)
        {
            try
            {
                var isExist = await _unitOfWork.User.GetOneByFilter(u => u.Email == addUserDTO.Email);
                if (isExist == null)
                {
                    User user = new();
                    user.Name = addUserDTO.Name;
                    user.Email = addUserDTO.Email;
                    var newUser = await _unitOfWork.User.Add(user);

                    UserRole role = new UserRole();
                    role.UserId = newUser.Id;
                    role.RolesId = addUserDTO.RoleId;
                    var newUserRole = await _unitOfWork.UserRole.Add(role);
                    await _unitOfWork.Complete();

                    prepareSession(newUserRole);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    prepareSession(await _unitOfWork.UserRole.GetOneByFilter(u => u.UserId == isExist.Id));
                }
            }
            catch (Exception ex) 
            {
            
            }
            
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Data = await _unitOfWork.User.GetAll();
            return View(Data);
        }

        [NonAction]
        public bool CreateSession(Dictionary<string, string> data)
        {
            try
            {
                foreach (var element in data)
                {
                    _httpContextAccessor.HttpContext.Session.SetString(element.Key, element.Value);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [NonAction]
        public void prepareSession(UserRole userRole)
        {
            Dictionary<string, string> datd = new Dictionary<string, string>();
            datd.Add("userId",userRole.UserId.ToString());
            datd.Add("roleId",userRole.RolesId.ToString());
            var done = CreateSession(datd);
        }
    }
}
