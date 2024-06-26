/*
' Copyright (c) 2024 Redlime Solutions
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using Redlime.Modules.QuestionModule.Components;
using Redlime.Modules.QuestionModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Redlime.Modules.QuestionModule.Controllers
{
    [DnnHandleError]
    public class QuestionController : DnnController
    {


        #region UNUSED SECTION
        public ActionResult Edit(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var userlist = UserController.GetUsers(PortalSettings.PortalId);
            //var users = from user in userlist.Cast<UserInfo>().ToList()
            //            select new SelectListItem { Text = user.DisplayName, Value = user.UserID.ToString() };

            ViewBag.QuestionTypes = new List<SelectListItem>
            {
                 new SelectListItem {Text = "True Or False",Value = "TRUE_FALSE"  },
                 new SelectListItem {Text = "Multiple Choice",Value = "MULTIPLE_CHOICE"  }
            };

            var item = (itemId == -1)
                 ? new Question { ModuleId = ModuleContext.ModuleId }
                 : QuestionManager.Instance.GetQuestion(itemId, ModuleContext.ModuleId);

            return View(item);
        }

        //[DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        //[DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Add(object item)
        {
            //if (item.Id == -1)
            //{
            //    item.CreatedByUserId = User.UserID;
            //    item.CreatedOnDate = DateTime.UtcNow;
            //    item.LastModifiedByUserId = User.UserID;
            //    item.LastModifiedOnDate = DateTime.UtcNow;

            //    QuestionManager.Instance.CreateQuestion(item);
            //}
            //else
            //{
            //    var existingQuestion = QuestionManager.Instance.GetQuestion(item.Id, item.ModuleId);
            //    existingQuestion.LastModifiedByUserId = User.UserID;
            //    existingQuestion.LastModifiedOnDate = DateTime.UtcNow;
            //    existingQuestion.QuestionTitle = item.QuestionTitle;

            //    QuestionManager.Instance.UpdateQuestion(existingQuestion);
            //}
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(Question item)
        {
            if (item.Id == -1)
            {
                item.CreatedByUserId = User.UserID;
                item.CreatedOnDate = DateTime.UtcNow;
                item.LastModifiedByUserId = User.UserID;
                item.LastModifiedOnDate = DateTime.UtcNow;

                QuestionManager.Instance.CreateQuestion(item);
            }
            else
            {
                var existingQuestion = QuestionManager.Instance.GetQuestion(item.Id, item.ModuleId);
                existingQuestion.LastModifiedByUserId = User.UserID;
                existingQuestion.LastModifiedOnDate = DateTime.UtcNow;
                existingQuestion.QuestionTitle = item.QuestionTitle;

                QuestionManager.Instance.UpdateQuestion(existingQuestion);
            }

            return RedirectToDefaultRoute();
        }

        #endregion





        [ModuleAction(ControlKey = "Edit", TitleKey = "AddQuestion")]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetJsonResult()
        {
            return new JsonResult()
            {
                Data = new
                {
                    Name = "Shohan",
                    Number = "01854263181"
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        [HttpPost]
        public JsonResult GetTransactionInformation(long id)
        {
            var jsonData = new { success = false, message = "Current Account Not Found! Please Try Again." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddQuestion(Question obj)
        {
            obj.ModuleId = ModuleContext.ModuleId;


            bool response;
            if (obj.Id == 0)
            {
                obj.CreatedByUserId = User?.UserID ?? -1;
                obj.CreatedOnDate = DateTime.UtcNow;
                response = QuestionManager.Instance.CreateQuestion(obj);
            }
            else
            {

                obj.LastModifiedByUserId = User?.UserID ?? -1;
                obj.LastModifiedOnDate = DateTime.UtcNow;
                response = QuestionManager.Instance.UpdateQuestion(obj);
            }
            var jsonData = new { success = response, payload = obj, message = response ? "Operation successfull." : "Operation Failed." };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var data = QuestionManager.Instance.GetQuestion(id, ModuleContext.ModuleId);
            if (data != null)
            {
                var obj = new
                {
                    success = true,
                    data.QuestionTitle,
                    data.QuestionType,
                    data.Id,
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var obj = new { success = false, msg = "Operation Failed !" };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetList()
        {
            var obj = QuestionManager.Instance.GetQuestions(ModuleContext.ModuleId).AsEnumerable().Select(x => new
            {
                x.Id,
                x.QuestionTitle,
                x.QuestionType,
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
           var response = QuestionManager.Instance.DeleteQuestion(id, ModuleContext.ModuleId);
            var obj = new { success = response, msg = response? "Data deleted succesfully !": "Operation Failed !" };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
