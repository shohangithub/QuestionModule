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

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Caching;

namespace Redlime.Modules.QuestionModule.Models
{
    [TableName("QuestionModule_Questions")]
    //setup the primary key for table
    [PrimaryKey("Id", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Questions", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class Question
    {
        ///<summary>
        /// The ID of your object with the name of the QuestionName
        ///</summary>
        public int Id { get; set; } = -1;
        public string QuestionTitle{ get; set; }
        public string QuestionType { get; set; }
        
        ///<summary>
        /// The ModuleId of where the object was created and gets displayed
        ///</summary>
        public int ModuleId { get; set; }

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int CreatedByUserId { get; set; } = -1;

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int LastModifiedByUserId { get; set; } = -1;

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;
    }

    public class QuestionDTO
    {
        public int Id { get; set; } = -1;
        public string QuestionTitle { get; set; }
        public string QuestionType { get; set; }
        //public int ModuleId { get; set; }
        //public int CreatedByUserId { get; set; } = -1;
        //public int LastModifiedByUserId { get; set; } = -1;
        //public DateTime CreatedOnDate { get; set; } = DateTime.UtcNow;
        //public DateTime LastModifiedOnDate { get; set; } = DateTime.UtcNow;
    }
}
