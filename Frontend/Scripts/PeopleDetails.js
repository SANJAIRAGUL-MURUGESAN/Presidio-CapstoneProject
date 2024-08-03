const isloggedin = localStorage.getItem('token')
if(!isloggedin){
    Toastify({
        text: "Hey User! You are already Not logged In, Redirecting...",
        style: {
            fontSize: "15px",
            background: "linear-gradient(to right, #00b09b, #96c93d)",
        },
        callback: function() {
            window.location.href = 'Login.html'; // Redirect after toast disappears
        }
    }).showToast();
}



document.getElementById('usernamenav').innerHTML = localStorage.getItem('username')
document.getElementById('userprofileimgnav').src = localStorage.getItem('userprofileimglink')
// document.getElementById('userprofileimgnav2').src = localStorage.getItem('userprofileimglink')
document.getElementById('userimagemodal').src = localStorage.getItem('userprofileimglink')


document.addEventListener('DOMContentLoaded', async function() {

    const spinnerEl = document.querySelector('.spinnerborderpeople');
    spinnerEl.style.display = 'flex';

    async function fetchPeoples() {
        await fetch('https://localhost:7186/api/User/TopUsers', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(localStorage.getItem('userid'))
        })
        .then(async (response) => {
            const spinnerEl = document.querySelector('.spinnerborderpeople');
            spinnerEl.style.display = 'none';
            if (response.ok) {
                const data = await response.json();
                console.log(data);
                renderfollowpeopledate(data)
            } else {
                console.error('HTTP error', response.status, response.statusText);
            }
        })
        .catch(error => {
            console.error('Fetch error', error);
        });
    }

    async function fetchUserSideBarInfo() {
        await fetch('https://localhost:7186/api/User/UserSideBarInfo', {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer '+localStorage.getItem('token'),
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(localStorage.getItem('userid'))
        })
        .then(async (response) => {
            if (response.ok) {
                const data = await response.json();
                console.log(data);
                document.getElementById('username-sidebar').innerHTML = data.userName
                document.getElementById('puserid-sidebar').innerHTML = data.pUserId
                document.getElementById('userfollowing-sidebar').innerHTML = data.followingCount
                document.getElementById('userfollowers-sidebar').innerHTML = data.followersCount
                document.getElementById('useimage-sidebar').src = data.userProfileImgLink
            } else {
                console.error('HTTP error', response.status, response.statusText);
            }
        })
        .catch(error => {
            console.error('Fetch error', error);
        });
    }
    await fetchPeoples()
    await fetchUserSideBarInfo()
    await NotificationInfo()
})

function renderfollowpeopledate(users) {
    const followContainer = document.getElementById('follow-container');
    // followContainer.innerHTML = '';

    users.forEach(user => {

        const followUserDiv = document.createElement('div');
        followUserDiv.className = 'follow-user';
        
        const followUserImgDiv = document.createElement('div');
        followUserImgDiv.className = 'follow-user-img';
        const img = document.createElement('img');
        img.src = user.userProfileLink;
        followUserImgDiv.appendChild(img);
        
        const followUserInfoDiv = document.createElement('div');
        followUserInfoDiv.className = 'follow-user-info';
        const h4 = document.createElement('h4');
        h4.textContent = user.userName;
        const p = document.createElement('p');
        p.textContent = `@${user.pUserId}`;
        followUserInfoDiv.appendChild(h4);
        followUserInfoDiv.appendChild(p);

        h4.style.cursor = "pointer"
        h4.addEventListener('mouseenter', function() {
            h4.style.textDecoration = "underline";
        });
        
        h4.addEventListener('mouseleave', function() {
            h4.style.textDecoration = "none";
        });
        h4.addEventListener('click', async function() {
            localStorage.setItem('profilepagedisplayeruserid',user.userId)
            // window.open("TwitterProfile.html");
            window.location.href = "TwitterProfile.html"
        })
        
        const followButton = document.createElement('button');
        followButton.type = 'button';
        followButton.className = 'follow-btn';

        if(user.isFollowedByUser == "Yes"){
            followButton.textContent = 'Following';
            followButton.style.cursor = 'pointer'
        }else if(user.isFollowedByUser == "No"){
              followButton.textContent = 'Follow';
            followButton.style.cursor = 'pointer'
        }


        followButton.addEventListener('click', async function() {
            console.log('asdf')
            console.log(followButton.textContent)
            if(followButton.textContent == "Following"){
                await RemoveFollowRequest(user.userId)
                followButton.textContent = 'Follow';
            }else if(followButton.textContent == "Follow"){
                await AddFollowRequest(user.userId)
                followButton.textContent = 'Following';
                followButton.style.fontSize = '14px'
            }
        })
        
        followUserDiv.appendChild(followUserImgDiv);
        followUserDiv.appendChild(followUserInfoDiv);
        followUserDiv.appendChild(followButton);
        
        followContainer.appendChild(followUserDiv)
    });
    const followLinkDiv = document.createElement('div');
    followLinkDiv.className = 'follow-link';
    const link = document.createElement('a');
    link.href = '';
    link.textContent = 'Show more';
    followLinkDiv.appendChild(link);

    const footer = document.createElement('footer');
    footer.className = 'follow-footer';
    const ul = document.createElement('ul');

    const terms = document.createElement('li');
    const termsLink = document.createElement('a');
    termsLink.href = '#';
    termsLink.textContent = 'Terms';
    terms.appendChild(termsLink);

    const privacy = document.createElement('li');
    const privacyLink = document.createElement('a');
    privacyLink.href = '#';
    privacyLink.textContent = 'Privacy Policy';
    privacy.appendChild(privacyLink);

    const cookies = document.createElement('li');
    const cookiesLink = document.createElement('a');
    cookiesLink.href = '#';
    cookiesLink.textContent = 'Cookies';
    cookies.appendChild(cookiesLink);

    const about = document.createElement('li');
    const aboutLink = document.createElement('a');
    aboutLink.href = '#';
    aboutLink.textContent = 'About';
    about.appendChild(aboutLink);

    const more = document.createElement('li');
    const moreLink = document.createElement('a');
    moreLink.href = '#';
    moreLink.textContent = 'More';
    more.appendChild(moreLink);

    ul.appendChild(terms);
    ul.appendChild(privacy);
    ul.appendChild(cookies);
    ul.appendChild(about);
    ul.appendChild(more);
    footer.appendChild(ul);
    followContainer.appendChild(followLinkDiv)
    followContainer.appendChild(footer)
}

async function AddFollowRequest(followerid) {

    await fetch('https://localhost:7186/api/User/AddFollow', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "followerId": followerid,
            "userId": localStorage.getItem('userid')
        })
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            Toastify({
                text: "Following Successfully",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
        //   alert('Following Successfully')

        }
    }).catch(error => {
        console.error(error);
    });
}

async function RemoveFollowRequest(followerid) {

    await fetch('https://localhost:7186/api/User/RemoveFollow', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "followerId": followerid,
            "userId": localStorage.getItem('userid')
        })
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            Toastify({
                text: "Unfollowed Successfully",
                style: {
                    fontSize: "15px",
                    background: "linear-gradient(to right, #00b09b, #96c93d)",
                }
               }).showToast();
        //   alert('Unfollowed Successfully')

        }
    }).catch(error => {
        console.error(error);
    });
}

// Sidebar Info

async function NotificationInfo() {
    var flag=0;
    await fetch('https://localhost:7186/api/User/UserNotifications', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            "userId": localStorage.getItem('userid')
        })
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            var data = await response.json();
            console.log(data)
            const reverseddata =data.reverse();
            reverseddata.forEach(async notification => {
                if(notification.isUserSeen == "No"){
                    flag=1;
                }
            })
        }
    }).catch(error => {
        console.error(error);
    });
    console.log("flag",flag)
    if(flag==1){
        var n = document.getElementById('notifi')
        n.style.color = '#1aa1f5'
    }
}

// function renderUserNotificatio(notification) {

//     const head = document.getElementById('header-notifi')

//     const header = document.createElement('div');
//     header.classList = 'header-post '

//     const imgdiv = document.createElement('div');
//     imgdiv.classList = 'header-img-wrapper'

//     const img = document.createElement('img')
//     img.src = notification.notificationPost

//     imgdiv.appendChild(img)

//     const Content = document.createElement('div');
//     Content.classList = 'input'
//     Content.innerHTML = notification.notificatioContent

//     header.appendChild(imgdiv)
//     header.appendChild(Content)

//     head.appendChild(header)

// }

const profilebutton = document.getElementById('profilepagedisplayer')
profilebutton.addEventListener('click',async() => {
    localStorage.setItem('profilepagedisplayeruserid',localStorage.getItem('userid'))
     window.location.href = "TwitterProfile.html"
    // window.open("TwitterProfile.html");
})

// document.getElementById('search-p-user-input').addEventListener("keyup", async function() {
//     // const value = this.value;
//     // // Call your function here with the value
//     // yourFunction(value);
//     const v = document.getElementById('search-p-user-input')
//     // console.log()
//     if(v.value != null){
//         console.log('yes')
//     }else{
//         console.log('no')
//     }
//   });
document.getElementById('search-icon').style.cursor = 'pointer'

document.getElementById('search-icon').addEventListener("click", async function() {
    localStorage.setItem('serachusername',document.getElementById('search-p-user-input').value)
    // window.open("PeopleSearch.html");
    document.getElementById('search-p-user-input').value = ""
    window.location.href = "PeopleSearch.html"
})

document.getElementById('logout').addEventListener('click',async()=>{
    async function clearAllLocalStorage() {
        const storage = window.localStorage;
        const keys = Object.keys(storage); // Get all keys as an array
      
        for (const key of keys) {
          storage.removeItem(key);
        }
      }
      await clearAllLocalStorage()
      window.location.href = "Login.html"
})