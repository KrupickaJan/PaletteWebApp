// Color palette functionality

document.addEventListener('DOMContentLoaded', function () {
    const colorAreas = document.querySelectorAll('.colorArea');

    colorAreas.forEach(colorArea => {
        colorArea.addEventListener('mouseenter', handleMouseEnter);
        colorArea.addEventListener('mouseleave', handleMouseLeave);
    });

    function handleMouseEnter(event) {
        const colorArea = event.currentTarget;
        const currentWidth = colorArea.getBoundingClientRect().width;

        if (currentWidth <100) {
            colorArea.style.flex = '0 0 100px';
        }
        const label = colorArea.querySelector('.colorLabel');
        label.style.opacity = '1';
    }

    function handleMouseLeave(event) {
        colorAreas.forEach(area => {
            area.style.flex = '1';
            const label = area.querySelector('.colorLabel');
            label.style.opacity = '0';
        });
    }
});


// File upload functionality

const dropArea = document.getElementById('dropArea');
const dropAreaText = document.getElementById('dropAreaText');
const fileInput = document.getElementById('fileInput');
const uploadButton = document.getElementById('uploadButton');
const uploadForm = document.getElementById('uploadForm');
let selectedFile;

dropArea.addEventListener('dragover', (event) => {
    event.preventDefault();
    dropArea.style.borderColor = '#000';
});

dropArea.addEventListener('dragleave', () => {
    dropArea.style.borderColor = '#ccc';
});

dropArea.addEventListener('drop', (event) => {
    event.preventDefault();
    dropArea.style.borderColor = '#ccc';
    const files = event.dataTransfer.files;
    if (files.length > 0) {
        handleFileSelect(files[0]);
    }
});

dropArea.addEventListener('click', () => {
    fileInput.click();
});

fileInput.addEventListener('change', (event) => {
    const files = event.target.files;
    if (files.length > 0) {
        handleFileSelect(files[0]);
    }
});

uploadForm.addEventListener('submit', (event) => {
    if (!selectedFile) {
        event.preventDefault();
        alert('No file selected!');
    }
});

function handleFileSelect(file) {
    const reader = new FileReader();
    reader.onload = (event) => {
        dropArea.style.backgroundImage = `url(${event.target.result})`;
        dropAreaText.style.display = 'none';
        uploadButton.disabled = false;
        dropArea.style.border = 'none';
    };
    reader.readAsDataURL(file);
    selectedFile = file;
    fileInput.files = createFileList(file);
}

function createFileList(file) {
    const dataTransfer = new DataTransfer();
    dataTransfer.items.add(file);
    return dataTransfer.files;
}