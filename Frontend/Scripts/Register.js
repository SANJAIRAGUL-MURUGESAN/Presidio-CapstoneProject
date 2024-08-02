const uname = document.querySelector('#namer');
const username = document.querySelector('#usernamer');
const userpassword = document.querySelector('#userpasswordr');
const useremail = document.querySelector('#useremailr');
const userlocation = document.querySelector('#userlocationr');
const userphone = document.querySelector('#userphoner');
const userbior = document.querySelector('#userbior');
const usergender = document.querySelector('#gender');
const userdobr = document.querySelector('#userdobr');

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

    const usernameVal = uname.value.trim()
    const username2Val = username.value.trim()
    const emailVal = useremail.value.trim();
    const passwordVal = userpassword.value.trim();
    const phonenumberVal = userphone.value.trim();
    const userbiorVal = userbior.value.trim();

    let success = true

    var flag = 0;
    if(usernameVal===''){
        flag=1;
        alert('Name is required')
    }

    if(emailVal===''){
        flag=1;
        alert('Email is required')
    }
    else if(!validateEmail(emailVal)){
        flag=1;
        alert('Please enter a valid email')
    }


    if(passwordVal === ''){
        flag=1;
        alert('Password is required')
    }
    else if(passwordVal.length<3){
        flag=1;
        alert('Password must be atleast 3 characters long')
    }

    function validateNumber(input) {
        const numberRegex = /^\d+$/;
        return numberRegex.test(input);
    }

 
    if(phonenumberVal === ''){
        flag=1;
        alert('Phone Number is required')
    }
    else if (!validateNumber(phonenumberVal)) {
        flag=1;
        alert('Only Numbers are allowed')
    }
    else if(phonenumberVal.length>10){
        flag=1;
        alert('Phone Number should not Exeed 10 Numbers')
    }
    else if(phonenumberVal.length<10){
        flag=1;
        alert('Phone Number should not be less than 10 Numbers')
    }

    if(username2Val === ''){
        flag=1;
        alert('Username is required')
    }
    else if(username2Val.length>15){
        flag=1;
        alert('Username should not Exeed 15 Characters')
    }

    if(userbiorVal === ''){
        flag=1;
        alert('User Bio is required')
    }
    else if(userbiorVal.length>30){
        flag=1;
        alert('User Bio should not Exeed 30 Characters')
    }


    function userRegsiter(){
        fetch('https://localhost:7186/api/User/RegisterUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
             },
            body: JSON.stringify({
                "userName": uname.value.trim(),
                "userId": username.value.trim(),
                "userEmail": useremail.value.trim(),
                "userPassword": userpassword.value.trim(),
                "location": userlocation.value.trim(),
                "userMobile": userphone.value.trim(),
                "userGender": usergender.value,
                "isPremiumHolder": "No",
                "dateOfBirth": userdobr.value,
                "age": 0,
                "bioDescription": userbior.value.trim(),
                "userProfileImgLink": "https://sanjaistorageblob.blob.core.windows.net/pictures/dummyprofile.png"
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
                // alert('Hey User, Registration Successful!');
                document.getElementById("formr").reset();
                // window.open("Login.html");
                Toastify({
                    text: "Hey User! Registration Successful, Redirecting...",
                    style: {
                        fontSize: "15px",
                        background: "linear-gradient(to right, #00b09b, #96c93d)",
                    },
                    callback: function() {
                        window.location.href = 'Login.html'; // Redirect after toast disappears
                    }
                }).showToast();
            }
        })
        .catch(error => {
            alert(error);
        });
    }

    if(flag==0){
        userRegsiter()
    }
})


const validateEmail = (email) => {
    return String(email)
      .toLowerCase()
      .match(
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
      );
  };
