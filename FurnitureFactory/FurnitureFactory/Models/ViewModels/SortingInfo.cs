using System;
using System.Collections.Generic;
using System.Linq;

namespace FurnitureFactory.Models.ViewModels
{
    public class SortingInfo
    {
        public string SortDirection { get; set; }
        public string SortField { get; set;}
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set;}
        public string SearchTerm { get; set; }

        public SortingInfo()
        {
            SortDirection = "Ascending";
            SortField = "Name";
            PageSize = 10;
            PageCount = 10;
            CurrentPageIndex = 1;
            SearchTerm = null;
        }

        public static SortingInfo SetProperties(SortingInfo sortInfo, int pageIndex, string searchTerm, string sortField)
        {
            sortInfo.CurrentPageIndex = pageIndex;
            sortInfo.SearchTerm = searchTerm;

            if (sortInfo.SortField == sortField)
            {
                if (sortInfo.SortDirection == "Ascending")
                    sortInfo.SortDirection = "Descending";
                else
                {
                    sortInfo.SortDirection = "Ascending";
                }
            }
            else
            {
                sortInfo.SortField = sortField;
                sortInfo.SortDirection = "Ascending";
            }
            return sortInfo;
        }

        internal IQueryable<Furniture> SortQuery(IQueryable<Furniture> query)
        {
            IQueryable<Furniture> sortedQuery = null;
            switch (SortField)
            {
                case "Name":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(f => f.Name) :
                              query.OrderByDescending(f => f.Name));
                    break;
                case "Price":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(f => f.PricePerUnit) :
                              query.OrderByDescending(f => f.PricePerUnit));
                    break;
                case "Description":
                    sortedQuery = (SortDirection == "Ascending" ?
                             query.OrderBy(f => f.Description) :
                             query.OrderByDescending(f => f.Description));
                    break;
                case "Weight":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(f => f.Weight) :
                              query.OrderByDescending(f => f.Weight));
                    break;
                case "BarCode":
                    sortedQuery = (SortDirection == "Ascending" ?
                        query.OrderBy(f => f.BarCode) :
                        query.OrderByDescending(f => f.BarCode));
                    break;
            }
            return sortedQuery;
        }

        internal IQueryable<ApplicationUser> SortQuery(IQueryable<ApplicationUser> query)
        {
            IQueryable<ApplicationUser> sortedQuery = null;
            switch (SortField)
            {
                case "UserName":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(u => u.UserName) :
                              query.OrderByDescending(u => u.UserName));
                    break;
                case "FullName":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(u => u.FullName) :
                              query.OrderByDescending(u => u.FullName));
                    break;
                case "Adress":
                    sortedQuery = (SortDirection == "Ascending" ?
                             query.OrderBy(u => u.Adress) :
                             query.OrderByDescending(u => u.Adress));
                    break;
                case "Bulstat":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(u => u.Bulstat) :
                              query.OrderByDescending(u => u.Bulstat));
                    break;
                case "MOL":
                    sortedQuery = (SortDirection == "Ascending" ?
                        query.OrderBy(u => u.MOL) :
                        query.OrderByDescending(u => u.MOL));
                    break;
            }
            return sortedQuery;
        }

        internal IQueryable<Order> SortQuery(IQueryable<Order> query)
        {
            IQueryable<Order> sortedQuery = null;
            switch (SortField)
            {
                case "ClientName":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(o => o.Client.UserName) :
                              query.OrderByDescending(o => o.Client.UserName));
                    break;
                case "Date":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(o=> o.Date) :
                              query.OrderByDescending(o => o.Date));
                    break;
                case "TotalPrice":
                    sortedQuery = (SortDirection == "Ascending" ?
                             query.OrderBy(o=> o.TotalPrice) :
                             query.OrderByDescending(o => o.TotalPrice));
                    break;
                case "RecieptNumber":
                    sortedQuery = (SortDirection == "Ascending" ?
                              query.OrderBy(o => o.RecieptNumber) :
                              query.OrderByDescending(o => o.RecieptNumber));
                    break;
            }
            return sortedQuery;
        }
    }
}