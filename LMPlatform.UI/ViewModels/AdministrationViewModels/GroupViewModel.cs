﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Application.Core;
using Application.Infrastructure.GroupManagement;
using LMPlatform.Models;
using LMPlatform.UI.Attributes;

namespace LMPlatform.UI.ViewModels.AdministrationViewModels
{
    public class GroupViewModel
    {
        private readonly LazyDependency<IGroupManagementService> _groupManagementService = new LazyDependency<IGroupManagementService>();

        private IGroupManagementService GroupManagementService
        {
            get
            {
                return _groupManagementService.Value;
            }
        }

        [DisplayName("Номер")]
        [Required(ErrorMessage = "Поле Номер обязательно для заполнения")]
        public string Name { get; set; }

        [DisplayName("Год поступления")]
        [Required(ErrorMessage = "Поле Год поступления обязательно для заполнения")]
        public string StartYear { get; set; }

        [DisplayName("Год выпуска")]
        [Required(ErrorMessage = "Поле Год выпуска обязательно для заполнения")]
        [GreaterThan("StartYear", ErrorMessage = "Значение поля Год выпуска должен быть больше Года поступления")]
        public string GraduationYear { get; set; }

        [DisplayName("")]
        public HtmlString HtmlLinks { get; set; }

        public int Id { get; set; }

        public GroupViewModel()
        {
        }

        public GroupViewModel(Group group)
        {
            Name = group.Name;
            StartYear = group.StartYear;
            GraduationYear = group.GraduationYear;
        }

        public IList<SelectListItem> GetYears()
        {
            var actualYear = DateTime.Now.Year;
            var yearsList = new List<SelectListItem>();

            for (int year = actualYear - 10; year < actualYear + 10; year++)
            {
                yearsList.Add(new SelectListItem()
                    {
                        Text = year.ToString(),
                        Value = year.ToString(),
                    });
            }

            return yearsList;
        }

        public static GroupViewModel FormGroup(Group group, string htmlLinks)
        {
            return new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                StartYear = group.StartYear,
                GraduationYear = group.GraduationYear,
                HtmlLinks = new HtmlString(htmlLinks)
            };
        }

        public void AddGroup()
        {
            GroupManagementService.AddGroup(GetGroupFromViewModel());
        }

        public void ModifyGroup(int groupId)
        {
            GroupManagementService.UpdateGroup(new Group()
            {
                Id = groupId,
                Name = Name,
                GraduationYear = GraduationYear,
                StartYear = StartYear
            });
        }

        private Group GetGroupFromViewModel()
        {
            return new Group()
              {
                  Name = Name,
                  GraduationYear = GraduationYear,
                  StartYear = StartYear
              };
        }
    }
}