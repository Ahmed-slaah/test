using DemoProject.Core.DTOs;
using DemoProject.Core.Models;
using DemoProject.Models;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace DemoProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;



        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;

        }


        [HttpGet]
        public async Task<IActionResult> Index(string search, int page=1, int pageSize = 3)
        {
            var userId = GetSession("userId");
            var roleId = GetSession("roleId");
            string[] Includes = { "Categories" };
            int DataCount;
            IEnumerable<Vacancies> myData = new List<Vacancies>();
            GetVacanciesListDTO getVacanciesListDTO = new GetVacanciesListDTO();

            if (search!=null)
            {
                myData = await _unitOfWork.Vacancies.FindAllIcludedPagiantion(n=>n.Name.Contains(search),page,pageSize,Includes);
                DataCount = _unitOfWork.Vacancies.GetCount(n => n.Name.Contains(search));
            }
            else
            {
                myData = await _unitOfWork.Vacancies.FindAllIcludedPagiantion( page, pageSize, Includes);
                DataCount = _unitOfWork.Vacancies.GetCount();
            }

            getVacanciesListDTO = prepareVacanciewDtoList(myData);
            getVacanciesListDTO.count = DataCount;
            getVacanciesListDTO.userId = userId;
            getVacanciesListDTO.roleId = roleId;
            getVacanciesListDTO.pageSize = pageSize;

            return View(getVacanciesListDTO);
        }

       
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {

            var userId = GetSession("userId");
            var roleId = GetSession("roleId");
            try
            {
                var status = await _unitOfWork.Vacancies.DeleteRequest(Id, roleId);
                _unitOfWork.Complete();

            }
            catch (Exception)
            {

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> DeletedRequests()
        {

            var userId = GetSession("userId");
            var roleId = GetSession("roleId");
            if(roleId == "1")
            {
                try
                {

                    var myData = await _unitOfWork.Vacancies.GetByFilter(a=>a.IsRequestedToCancel == true);
                    GetVacanciesListDTO getVacanciesListDTO = new GetVacanciesListDTO();
                    getVacanciesListDTO = prepareVacanciewDtoList(myData);
                    getVacanciesListDTO.userId = userId;
                    getVacanciesListDTO.roleId = roleId;
                    getVacanciesListDTO.count = myData.Count();
                    return View(getVacanciesListDTO);
                    

                }
                catch (Exception)
                {

                }
            }
           
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddVacancyDTO addVacancyDTO = new AddVacancyDTO();
            
            addVacancyDTO.Categories = new List<CategoryDTO>();
            var Data = await _unitOfWork.Categories.GetAll();
            foreach (var Category in Data)
            {
                CategoryDTO categoryDTO = new CategoryDTO();
                categoryDTO.Name = Category.Name;
                categoryDTO.CategoryId = Category.CategoryId;
                addVacancyDTO.Categories.Add(categoryDTO);
            }
            return View(addVacancyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVacancyDTO addVacancyDTO)
        {
            try
            {
                Vacancies vacancies = new Vacancies();
                vacancies.IsRequestedToCancel = false;
                vacancies.OpenedFrom = DateTime.Now;
                vacancies.Name = addVacancyDTO.Name;
                vacancies.Descriptions = addVacancyDTO.Descriptions;
                vacancies.Resposibilities = addVacancyDTO.Resposibilities;
                vacancies.Skills = addVacancyDTO.Skills;
                vacancies.OpenedTo = addVacancyDTO.OpenedTo;
                vacancies.Max = addVacancyDTO.Max;
                vacancies.CategoryId = addVacancyDTO.CategoryId;
                vacancies.Categories = await _unitOfWork.Categories.GetOneByFilter(a=>a.CategoryId== addVacancyDTO.CategoryId);

                await _unitOfWork.Vacancies.Add(vacancies);
                await _unitOfWork.Complete();
            }catch(Exception ex)
            {

            }
            
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public async Task<IActionResult> update(int Id)
        {

            var data = await _unitOfWork.Vacancies.GetOneByFilter(a=>a.Id== Id);
            AddVacancyDTO addVacancyDTO = new AddVacancyDTO();
            if (data != null)
            {
                addVacancyDTO.id = data.Id;
                addVacancyDTO.Name = data.Name;
                addVacancyDTO.Descriptions = data.Descriptions;
                addVacancyDTO.Resposibilities = data.Resposibilities;
                addVacancyDTO.Skills = data.Skills;
                addVacancyDTO.Max = data.Max;
                addVacancyDTO.OpenedTo = data.OpenedTo;
                addVacancyDTO.CategoryId = data.CategoryId;
                addVacancyDTO.Categories = new List<CategoryDTO>();
                var Data = await _unitOfWork.Categories.GetAll();
                foreach (var Category in Data)
                {
                    CategoryDTO categoryDTO = new CategoryDTO();
                    categoryDTO.Name = Category.Name;
                    categoryDTO.CategoryId = Category.CategoryId;
                    addVacancyDTO.Categories.Add(categoryDTO);
                }


            }

            return View(addVacancyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddVacancyDTO addVacancyDTO)
        {
            try
            {
                var vacancies = await _unitOfWork.Vacancies.GetOneByFilter(a => a.Id == addVacancyDTO.id);
                vacancies.IsRequestedToCancel = vacancies.IsRequestedToCancel;
                vacancies.Name = addVacancyDTO.Name;
                vacancies.Descriptions = addVacancyDTO.Descriptions;
                vacancies.Resposibilities = addVacancyDTO.Resposibilities;
                vacancies.Skills = addVacancyDTO.Skills;
                vacancies.OpenedTo = addVacancyDTO.OpenedTo;
                vacancies.Max = addVacancyDTO.Max;
                vacancies.CategoryId = addVacancyDTO.CategoryId;
                vacancies.Categories = await _unitOfWork.Categories.GetOneByFilter(a => a.CategoryId == addVacancyDTO.CategoryId);

                await _unitOfWork.Vacancies.Update(vacancies);
                await _unitOfWork.Complete();
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public string GetSession(string Key)
        {
            try
            {
                string value = _httpContextAccessor.HttpContext.Session.GetString(Key);
                return value;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        [NonAction]
        public GetVacanciesListDTO prepareVacanciewDtoList(IEnumerable<Vacancies> myData)
        {
            GetVacanciesListDTO getVacanciesListDTO = new GetVacanciesListDTO();
            getVacanciesListDTO.VacanciesList = new List<GetVacanciesDTO>();

            foreach (var item in myData)
            {
                GetVacanciesDTO getVacanciesDTO = new GetVacanciesDTO();
                getVacanciesDTO.Name = item.Name;
                getVacanciesDTO.Categories = item.Categories;
                getVacanciesDTO.OpenedFrom = DateTime.Now;
                getVacanciesDTO.Descriptions = item.Descriptions;
                getVacanciesDTO.Resposibilities = item.Resposibilities;
                getVacanciesDTO.OpenedTo = item.OpenedTo;
                getVacanciesDTO.Id = item.Id;
                getVacanciesDTO.Categories = item.Categories;
                getVacanciesListDTO.VacanciesList.Add(getVacanciesDTO);
            }
            return getVacanciesListDTO;
        }
    }
}