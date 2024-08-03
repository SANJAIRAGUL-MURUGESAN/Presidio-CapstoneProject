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
    await fetch('https://localhost:7186/api/User/UserProfileDetails', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer '+localStorage.getItem('token'),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(localStorage.getItem('profilepagedisplayeruserid'))
    }).then(async (response) => {
        if (!response.ok) {
            throw new Error('Failed to update Dislike status');
        }else{
            var data = await response.json();
            console.log(data)
            document.getElementById('username-profile').innerHTML = data.userName
            document.getElementById('profilepic').src = data.userProfileImgLink
            document.getElementById('userdob').innerHTML = `<i class="fa fa-birthday-cake" aria-hidden="true"></i>  ${formatDate(data.dateOfBirth)}`
            document.getElementById('username-pid').innerHTML = `@${data.userId}`
            document.getElementById('desc').innerHTML = `${data.bioDescription} |`
            document.getElementById('userlocation').innerHTML =  ` <i class="fa fa-location-arrow "></i>  ${(data.location)}`
            document.getElementById('joinedon').innerHTML = `<i class="fa fa-calendar" aria-hidden="true"></i> Joined on:  ${formatDate(data.joinedDate)}`
        }
    }).catch(error => {
        console.error(error);
    });
})

function formatDate(dateString) {
    const months = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    // Split the date string into components
    const [year, month, day] = dateString.split('-');

    // Convert the month from 0-based index
    const monthName = months[parseInt(month, 10) - 1];

    // Format the date
    return `${monthName} ${parseInt(day, 10)}, ${year}`;
}

const dateStr = "2024-08-08";
const formattedDate = formatDate(dateStr);

console.log(formattedDate); // Output: August 8, 2024


let uploadedFiles = [];

document.getElementById('pimage').addEventListener('click', function () {
    document.getElementById('fileInput1').click();
});

document.getElementById('fileInput1').addEventListener('change', function (event) {
    document.getElementById('pimage').innerHTML = "Uploading"
    const files = event.target.files;
    const maxFiles = 1;
    // const imagePreviewContainer = document.getElementById('imagePreviewContainer');

    if (uploadedFiles.length + files.length > maxFiles) {
        alert('You can only upload a maximum of 1 image');
        return;
    }

    for (let i = 0; i < files.length; i++) {
        const file = files[i];
        uploadedFiles.push(file);

        const reader = new FileReader();
        reader.onload = function (e) {
            const img = document.createElement('img');
            img.src = e.target.result;
            img.className = 'image-preview';
            imagePreviewContainer.appendChild(img);
        };
        reader.readAsDataURL(file);
    }

    const formData = new FormData();

    for (let i = 0; i < uploadedFiles.length; i++) {
        console.log(uploadedFiles[i])
          formData.append('Images', uploadedFiles[i]);
          formData.append('userid',localStorage.getItem('userid'))
    }

    async function addFeedImages(){
        fetch('https://localhost:7186/api/User/AddTweetImage', {
            method: 'POST',
            body: formData
        })
        .then(async (response) => {
            var data = await response.json();
            console.log(data.userProfileImgLink)
            document.getElementById('pimage').innerHTML = "Uploaded"
            localStorage.setItem('userprofileimglink',data.userProfileImgLink)
            // alert('Hey User, Your Feed Added Successfully!!');
            // Reset the uploaded files and previews
            uploadedFiles = [];
            document.getElementById('fileInput1').value = '';
        })
        .catch(error => {
            console.error(error);
            alert('There was an error uploading the images.');
        });
     }
     addFeedImages()
});
