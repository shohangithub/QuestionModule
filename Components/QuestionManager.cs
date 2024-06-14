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
using System.Collections.Generic;

namespace Redlime.Modules.QuestionModule.Components
{
    internal interface IQuestionManager
    {
        void CreateQuestion(Question t);
        void DeleteQuestion(int itemId, int moduleId);
        void DeleteQuestion(Question t);
        IEnumerable<Question> GetQuestions(int moduleId);
        Question GetQuestion(int itemId, int moduleId);
        void UpdateQuestion(Question t);
    }

    internal class QuestionManager : ServiceLocator<IQuestionManager, QuestionManager>, IQuestionManager
    {
        public void CreateQuestion(Question t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
               
                var rep = ctx.GetRepository<Question>();

                rep.Insert(t);
                //var answer = ctx.GetRepository<QuestionAnswer>();
                //foreach (var item in t.Questions)
                //{
                //    answer.Insert(item);
                //}
            }
        }

        public void DeleteQuestion(int itemId, int moduleId)
        {
            var t = GetQuestion(itemId, moduleId);
            DeleteQuestion(t);
        }

        public void DeleteQuestion(Question t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Question>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Question> GetQuestions(int moduleId)
        {
            IEnumerable<Question> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Question>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public Question GetQuestion(int itemId, int moduleId)
        {
            Question t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Question>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateQuestion(Question t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Question>();
                rep.Update(t);
            }
        }

        protected override System.Func<IQuestionManager> GetFactory()
        {
            return () => new QuestionManager();
        }
    }
}
