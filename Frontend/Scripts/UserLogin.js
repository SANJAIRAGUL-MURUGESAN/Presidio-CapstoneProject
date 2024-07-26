const useremail = document.querySelector('#useremail');
const password = document.querySelector('#password');
const loginButton = document.querySelector('.login-form-btn')

loginButton.addEventListener('click', ()=>{

    console.log(useremail.value.trim())

    function userlogin(){
        fetch('https://localhost:7186/api/User/UserLogin', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "email": useremail.value.trim(),
                "password": password.value.trim()
            })
        })
        .then(async res => {
            const data = await res.json();
            console.log(data)
            if (!res.ok) {
                console.log(data.errorCode)
                alert(data.message);
            }else{
                localStorage.setItem('token',data.token);
                localStorage.setItem('userid',data.id);
                localStorage.setItem('usertype','user')
                localStorage.setItem('username',data.userName)
                localStorage.setItem('userprofileimglink',data.userProfileImgLink)
                alert('Hey User, Login Successful!');
                window.location.href = 'index.html';
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    userlogin()
})

