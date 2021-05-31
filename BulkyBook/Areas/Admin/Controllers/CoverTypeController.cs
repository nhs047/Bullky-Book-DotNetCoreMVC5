using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }
            //proc call
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            //proc call end
            //coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType objCoverType)
        {
            if (ModelState.IsValid)
            {

                //proc call
                var parameter = new DynamicParameters();
                parameter.Add("@Name", objCoverType.Name);
                //end proc call

                if (objCoverType.Id == 0)
                {
                    //proc call
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                    //end proc call

                    //_unitOfWork.CoverType.Add(objCategory);
                }
                else
                {
                    //proc call
                    parameter.Add("@Id", objCoverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);
                    //end proc call
                    //_unitOfWork.CoverType.Update(objCoverType);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(objCoverType);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            //proc call
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            //proc call end
            //var allObj = _unitOfWork.CoverType.GetAll();
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            //proc call
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            //proc call end


            //var objFromDb = _unitOfWork.CoverType.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //proc call
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            //proc call end


            //_unitOfWork.CoverType.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
