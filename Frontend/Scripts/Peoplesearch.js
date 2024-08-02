// const isloggedin = localStorage.getItem('token')
// if(!isloggedin){
//     Toastify({
//         text: "Hey User! You are already Not logged In, Redirecting...",
//         style: {
//             background: "linear-gradient(to right, #00b09b, #96c93d)",
//         },
//         callback: function() {
//             window.location.href = 'Login.html'; // Redirect after toast disappears
//         }
//     }).showToast();
// }




document.addEventListener('DOMContentLoaded', async function() {
    await NotificationInfo1()
    var n = document.getElementById('notifi')
    n.style.removeProperty('color');   
})

document.getElementById('search-icon').style.cursor = 'pointer'
document.getElementById('search-icon').addEventListener("click", async function() {
    const v = document.getElementById('search-p-user-input')
})

async function NotificationInfo1() {
    console.log('input',localStorage.getItem('serachusername'))
    console.log('hi')
    await fetch('https://localhost:7186/api/User/UserSerachProfiles', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body:  JSON.stringify(localStorage.getItem('serachusername'))
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            var data = await response.json();
            console.log(data)
            if(data.length==0){
                document.getElementById('NotweetText').style.display = 'flex';
            }else{
                data.forEach(async notification => {
                    await renderUserNotificatio(notification)
                })
            }
        }
    }).catch(error => {
        console.error(error);
    });

    // async function UpdateNotifications() {
    //     await fetch('https://localhost:7186/api/User/UpdateUserNotifications', {
    //         method: 'POST',
    //         headers: {
    //             'Authorization': 'Bearer '+localStorage.getItem('token'),
    //             'Content-Type': 'application/json',
    //         },
    //         body: JSON.stringify(localStorage.getItem('userid'))
    //     }).then(async (response) => {
    //         if (!response.ok) {
    //             throw new Error('Failed to update Dislike status');
    //         }else{
    //         }
    //     }).catch(error => {
    //         console.error(error);
    //     });
    // }
}

function renderUserNotificatio(notification) {

    const head = document.getElementById('header-notifi')

    const header = document.createElement('div');
    header.classList = 'header-post '
    head.style.cursor = "pointer"
    
    header.addEventListener('click', async function() {
        localStorage.setItem('profilepagedisplayeruserid',notification.id)
        window.open("TwitterProfile.html");
    })

    const imgdiv = document.createElement('div');
    imgdiv.classList = 'header-img-wrapper'

    const img = document.createElement('img')
    img.src = notification.userProfileImgLink

    imgdiv.appendChild(img)

    const Content = document.createElement('div');
    Content.classList = 'input'
    Content.innerHTML = `<span style="font-weight:bold">${notification.userName}</span><i class="fas fa-check-circle" style="font-size:13px;"></i>@${notification.userId} | <span style="font-size:13px">${notification.bioDescription}</span>`

    // const Time = document.createElement('div');
    // Time.classList = "notifi-time"
    // Time.innerHTML = `<span style="font-size:10px;"> ${timeAgo(notification.contentDateTime)}</span>`
    // Content.appendChild(Time)

    header.appendChild(imgdiv)

    header.appendChild(Content)


    head.appendChild(header)

}

