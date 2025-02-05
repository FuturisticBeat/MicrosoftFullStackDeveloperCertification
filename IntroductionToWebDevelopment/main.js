/* Singleton Pattern */
class Settings {

    constructor() {
        if(Settings.instance) {
            return Settings.instance;
        }
        this.configuration = {};
        Settings.instance = this;
    }

    set(key, value) {
        this.configuration[key] = value;
    }

    get(key) {
        return this.configuration[key];
    }
}

/* Module Pattern */
const CalculatorModule = (function(){
    function add(numA, numB) {
        return numA + numB;
    }

    function subtract(numA, numB) {
        return numA - numB;
    }

    return {
        add,
        subtract
    };
})();

/* Observer Pattern */
class Subject {
    constructor() {
        this.observers = [];
    }

    subscribe(observer) {
        this.observers.push(observer);
    }

    unsubscribe(observer) {
        this.observers = this.observers.filter(obs => obs != observer);
    }

    notify() {
        this.observers.forEach(observer => observer.update());
    }
}

class Observer {
    constructor(name) {
        this.name = name;
    }

    update() {
        console.log(`${this.name} received notification!`);
    }
}

/*
const settings = new Settings();
const otherSettings = new Settings();
console.log(settings === otherSettings); // true
console.log(CalculatorModule.add(2, 8));
const observerA = new Observer("A");
const observerB = new Observer("B");
const subject = new Subject();
subject.subscribe(observerA);
subject.subscribe(observerB);
subject.notify();
*/

/* Form Validation */
document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('.contact form');
    const nameInput = document.getElementById('name');
    const emailInput = document.getElementById('email');
    const messageInput = document.getElementById('message');

    form.addEventListener('submit', function(event) {
        event.preventDefault();
        let isValid = true;

        // Validate name
        if (nameInput.value.trim() === '') {
            isValid = false;
            alert('Name is required.');
        }

        // Validate email
        if (emailInput.value.trim() === '') {
            isValid = false;
            alert('Email is required.');
        } else if (!validateEmail(emailInput.value.trim())) {
            isValid = false;
            alert('Please enter a valid email address.');
        }

        // Validate message
        if (messageInput.value.trim() === '') {
            isValid = false;
            alert('Message is required.');
        }

        if (isValid) {
            alert('Form submitted successfully!');
            form.reset();
        }
    });

    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(email);
    }
});