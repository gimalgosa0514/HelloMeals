using mealplan.domain.users.exception;
using mealplan.domain.users.model;
using mealplan.domain.users.repository;
using mealplan.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.domain.users.service
{
    internal class UserServiceImpl : IUserService
    {

        private IUserRepository userRepository;
       
        public UserServiceImpl(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetAllUser()
        {
            return userRepository.SelectAllUser();
        }

        public User GetUserById(string loginId)
        {
            return userRepository.SelectUserByLoginId(loginId);
        }

        public bool Login(string inputId, string inputPassword)
        {
            User user = userRepository.SelectUserByLoginId(inputId);
            if(user == null)
            {
                throw new UserNotFoundException(ErrorMessage.LOGIN_EXCEPTION_MESSAGE);
            }
            if(user.Password != inputPassword)
            {
                throw new UserNotFoundException(ErrorMessage.LOGIN_EXCEPTION_MESSAGE);
            }
            
            // 로그인 했으니까, 올려놓고 true 반환
            Session.LoginedUser = user;
            return true;
        }

        public bool Logout(string inputId)
        {
            Session.LoginedUser = null;
            return true;
            
        }
    
        // 회원 탈퇴는 로그인 한 유저에 한 해 가능하니까. 탈퇴의 경우엔 세션에서 바로가져와서 지워버림.
        // 만약 회원 탈퇴가 아니라, 회원 삭제의 경우엔 메서드 하나 더 만들 것.
        public bool Withdraw()
        {
            if(Session.LoginedUser == null)
            {
                throw new UserNotFoundException(ErrorMessage.NOT_LOGINED_STATUS);
            }
            return userRepository.RemoveUser(Session.LoginedUser.LoginId);
        }


        // 본래는 DTO를 넘기는 것이 정석이지만,, 여기서는 앞단에서 유저정도는 만들어서 처리하자.
        public bool Regist(User user)
        {
            return userRepository.InsertUser(user);
        }
    }
}
