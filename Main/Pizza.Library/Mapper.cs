using Store.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pizza.Library
{
    public static class Mapper
    {


        public static User Map(UserInfo userInfo) => new User
        {
            //Id = userInfo.IdUser,
            //Name = Map(userInfo.FirstName).toList()
            
        };

        public static UserInfo Map(User restaurant) => new UserInfo
        {
           // Name = restaurant.Name,
           // Review = Map(restaurant.Reviews).ToList()
        };

        //public static IEnumerable<UserInfo> Map(IEnumerable<UserInfo> reviews) => reviews.Select(Map);

    }
}
