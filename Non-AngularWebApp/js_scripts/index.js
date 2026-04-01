//imports of modules and components go here

//global constant variables go here
let currentTheme = "dark";

//global state variables go here

//any dom element references go here
const toggleThemeButton = document.getElementById("darkModeToggle");

//event listeners go here
toggleThemeButton.addEventListener("click", () => {
    console.log("Toggle theme button clicked!");
});

Init();
//init function goes here
function Init() {
    console.log("Hello World!");
    console.log(toggleThemeButton);
}

//classes and public functions go here

//private functions go here