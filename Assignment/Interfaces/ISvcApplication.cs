using Assignment.Models;
using System.Collections.Generic;

namespace Assignment.Interfaces
{
    public interface ISvcApplication
    {
        List<Application> GetAllApplication();
        Application CreateApplication(Application application);
        Application GetApplicationById(string id);
        Application EditApplication(Application application);
    }
}
