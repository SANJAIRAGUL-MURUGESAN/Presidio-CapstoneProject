let uploadedFiles = [];

document.getElementById('imageIcon').addEventListener('click', function () {
    document.getElementById('fileInput').click();
});

document.getElementById('fileInput').addEventListener('change', function (event) {
    const files = event.target.files;
    const maxFiles = 4;
    const imagePreviewContainer = document.getElementById('imagePreviewContainer');

    if (uploadedFiles.length + files.length > maxFiles) {
        alert('You can only upload a maximum of 4 images');
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
});

document.querySelector('.modal-header button').addEventListener('click', function () {
    const formData = new FormData();

    if (uploadedFiles.length < 1 || uploadedFiles.length > 3) {
        alert('Please upload between 1 and 3 images.');
        return;
    }

    for (let i = 0; i < uploadedFiles.length; i++) {
      console.log(uploadedFiles[i])
        formData.append('images[]', uploadedFiles[i]);
    }

    // fetch('/upload-endpoint', {
    //     method: 'POST',
    //     body: formData
    // })
    // .then(response => response.json())
    // .then(data => {
    //     console.log(data);
    //     alert('Images uploaded successfully.');
    //     // Reset the uploaded files and previews
    //     uploadedFiles = [];
    //     document.getElementById('imagePreviewContainer').innerHTML = '';
    //     document.getElementById('fileInput').value = '';
    // })
    // .catch(error => {
    //     console.error(error);
    //     alert('There was an error uploading the images.');
    // });
});
