const useremail = document.querySelector('#useremail');
const password = document.querySelector('#password');
const loginButton = document.querySelector('.login-form-btn')

const isloggedin = localStorage.getItem('token')
if(isloggedin){
    Toastify({
        text: "Hey User! You are already logged In, Redirecting...",
        style: {
            fontSize: "15px",
            background: "linear-gradient(to right, #00b09b, #96c93d)",
        },
        callback: function() {
            window.location.href = 'index.html'; // Redirect after toast disappears
        }
    }).showToast();
}


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
                // alert(data.message);
                Toastify({
                    text: data.message,
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    }
                }).showToast();
            }else{
                localStorage.setItem('token',data.token);
                localStorage.setItem('userid',data.id);
                localStorage.setItem('usertype','user')
                localStorage.setItem('username',data.userName)
                localStorage.setItem('userprofileimglink',data.userProfileImgLink)
                // alert('Hey User, Login Successful!');
                // window.open("index.html");
                Toastify({
                    text: "Hey User! LogIn Successful, Redirecting...",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    },
                    callback: function() {
                        window.location.href = 'index.html'; // Redirect after toast disappears
                    }
                }).showToast();
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    userlogin()
})

