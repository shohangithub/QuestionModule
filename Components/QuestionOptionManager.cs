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

using DotNetNuke.Data;
using DotNetNuke.Framework;
using Redlime.Modules.QuestionModule.Models;
using System;
using System.Collections.Generic;

namespace Redlime.Modules.QuestionModule.Components
{
    internal interface IQuestionOptionManager
    {
        bool CreateQuestionOption(QuestionOption t);
        bool DeleteQuestionOption(int itemId, int moduleId);
        void DeleteQuestionOption(QuestionOption t);
        IEnumerable<QuestionOption> GetQuestionOptions(int moduleId);
        QuestionOption GetQuestionOption(int itemId, int moduleId);
        bool UpdateQuestionOption(QuestionOption t);
    }

    internal class QuestionOptionManager : ServiceLocator<IQuestionOptionManager, QuestionOptionManager>, IQuestionOptionManager
    {
        public bool CreateQuestionOption(QuestionOption t)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {

                    var rep = ctx.GetRepository<QuestionOption>();

                    rep.Insert(t);
                    //var answer = ctx.GetRepository<QuestionOptionAnswer>();
                    //foreach (var item in t.QuestionOptions)
                    //{
                    //    answer.Insert(item);
                    //}
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteQuestionOption(int itemId, int moduleId)
        {
            try
            {
                var t = GetQuestionOption(itemId, moduleId);
                DeleteQuestionOption(t);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void DeleteQuestionOption(QuestionOption t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionOption>();
                rep.Delete(t);
            }
        }

        public IEnumerable<QuestionOption> GetQuestionOptions(int moduleId)
        {
            IEnumerable<QuestionOption> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionOption>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public QuestionOption GetQuestionOption(int itemId, int moduleId)
        {
            QuestionOption t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<QuestionOption>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public bool UpdateQuestionOption(QuestionOption t)
        {
            try
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<QuestionOption>();
                    rep.Update(t);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected override System.Func<IQuestionOptionManager> GetFactory()
        {
            return () => new QuestionOptionManager();
        }
    }
}
