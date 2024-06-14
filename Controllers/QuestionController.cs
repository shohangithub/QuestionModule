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
using System.Web.Mvc;

namespace Redlime.Modules.QuestionModule.Controllers
{
    [DnnHandleError]
    public class QuestionController : DnnController
    {

        public ActionResult Delete(int itemId)
        {
            QuestionManager.Instance.DeleteQuestion(itemId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

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

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddQuestion")]
        public ActionResult Index()
        {
            ViewBag.QuestionType = new List<SelectListItem>
            {
                 new SelectListItem {Text = "True Or False",Value = "TRUE_FALSE"  },
                 new SelectListItem {Text = "Multiple Choice",Value = "MULTIPLE_CHOICE"  }
            };
            var items = QuestionManager.Instance.GetQuestions(ModuleContext.ModuleId);
            return View(items);
        }

        public JsonResult GetJsonResult()
        {
            return new JsonResult()
            {
                Data = new
                {
                  Name="Shohan",
                  Number="01854263181"
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

    }
}
