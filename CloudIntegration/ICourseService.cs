﻿using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;

namespace CloudIntegration
{
    public interface ICourseService
    {
        Task<List<SupportedCourse>> GetSupportedCoursesAsync();
        Task<List<Page>> GetPagesAsync(string projectId, string courseVersion = null);
        Task<Package> GetCoursePackageAsync(string projectId);
        Task<List<string>> GetCourseVersionsAsync(string projectId);
        List<Page> FilterPagesToIncludeOnlyItemsWithVersion(List<Page> pages, string courseVersion);

    }
}
