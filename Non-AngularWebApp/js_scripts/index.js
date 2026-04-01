//imports of modules and components go here

//global constant variables go here
let currentTheme = "dark";

//global state variables go here

//any dom element references go here
const toggleThemeButton = document.getElementById("darkModeToggle");

//event listeners go here
toggleThemeButton.addEventListener("click", () => toggleTheme());

Init();
//init function goes here
function Init() {
    console.log("Hello World!");
    console.log(toggleThemeButton);
}

function toggleTheme() {
    console.log("Button Clicked!");
    /*  set the backgroun color of the header, footer, and body to their white counter parts
        be sure to try and use a local cache to store a variable of some sort about the current theme 
        use that variable in storage to check what is currently being used and do the opposite*/
}
//classes and public functions go here

//private functions go here