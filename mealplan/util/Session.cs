using mealplan.domain.users.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mealplan.util
{
    
    // 로그인 후 정적 메모리 영역에 올려서 세션처럼 쓸 클래스임.
    public static class Session
    {
        
        // 일단은 어떤 정보가 필요할 지 모르니까 다 받아두자.. 비밀번호는 굳이 안받는게 맞지만, 일단은.
        public static User LoginedUser { get; set; }
    }
}
