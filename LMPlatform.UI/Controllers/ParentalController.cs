﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Core.Data;
using Application.Core.UI.HtmlHelpers;
using Application.Infrastructure.SubjectManagement;
using LMPlatform.Models;
using LMPlatform.UI.ViewModels.ParentalViewModels;
using Mvc.JQuery.Datatables;

namespace LMPlatform.UI.Controllers
{
    using Application.Core.UI.Controllers;
    using Application.Infrastructure.GroupManagement;
    using Application.Infrastructure.UserManagement;

    [AllowAnonymous]
    public class ParentalController : BasicController
    {
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            var group = GroupManagementService.GetGroupByName(id);
            
            if (group != null)
            {
                var model = new ParentalViewModel()
                {
                    Group = group
                };
                return View(model);
            }

            return RedirectToAction("GroupNotFound");
        }

        public ActionResult Plan(string id, int subjectId)
        {
            var group = GroupManagementService.GetGroupByName(id);
            if (group != null)
            {
                var model = new PlanViewModel(group, subjectId);
                
                return View(model);
            }

            return RedirectToAction("GroupNotFound");
        }

        public ActionResult Statistics(string id)
        {
            var group = GroupManagementService.GetGroupByName(id);
            if (group != null)
            {
                var model = new StatisticsViewModel(group);

                return View(model);
            }

            return RedirectToAction("GroupNotFound");
        }

        public ActionResult GetSideNav(int groupId)
        {
            var group = GroupManagementService.GetGroup(groupId);
            var subjects = SubjectManagementService.GetGroupSubjects(groupId);

            var model = new ParentalViewModel(group)
                {
                    Subjects = subjects
                };
            return PartialView("_ParentalSideNavPartial", model);
        }

        public List<Subject> GetSubjects(int groupId)
        {
            return SubjectManagementService.GetGroupSubjects(groupId);
        }

        public ActionResult GroupNotFound()
        {
            return View();
        }

        public bool IsValidGroup(string groupName)
        {
            return GroupManagementService.GetGroupByName(groupName) != null;
        }

        [HttpPost]
        public DataTablesResult<DataTableStat> GetStatCollection(DataTablesParam dataTableParam)
        {
            var searchString = dataTableParam.GetSearchString();
            bool? param = null;
            try
            {
                param = bool.Parse(Request.QueryString["param"]);
            }
            catch (ArgumentNullException)
            {
            }
            catch (FormatException)
            {
            }

            var models = new List<ModelBase>();
            return DataTableExtensions.GetResults(
                models.Select(m =>
                    new DataTableStat(PartialViewToString("_MessageDisplayRow", DisplayStatViewModel.FormStatToDisplay()))),
                    dataTableParam,
                    models.Count);
        }

        public IGroupManagementService GroupManagementService
        {
            get
            {
                return ApplicationService<IGroupManagementService>();
            }
        }

        public ISubjectManagementService SubjectManagementService
        {
            get
            {
                return ApplicationService<ISubjectManagementService>();
            }
        }
    }
}
