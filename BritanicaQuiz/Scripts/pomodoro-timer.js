var Timer = function (hours, minutes, seconds, onStopCallback) {

    this.hours = hours;
    this.minutes = minutes;
    this.seconds = seconds;

    this.audio = new Audio('../sounds/beep.mp3');

    if (onStopCallback) {
        this.gOnStopCallback = onStopCallback
    }
};

Timer.prototype.start = function () {
    this.stopTimer();
    this.resetTimer();
    this.startTimer();
}

Timer.prototype.finishTimer = function () {
    if (this.gOnStopCallback) {
        this.gOnStopCallback();
    };
}

Timer.prototype.startAlarm = function () {
    if (this.remainingTime < 1000) {
        this.audio.play();
    };
}

Timer.prototype.startTimer = function () {

    var that = this;

    this.countdownHandle = setInterval(function () {
        that.decrementTimer(that);
    }, 1000);
}

Timer.prototype.stopTimer = function () {
    clearInterval(this.countdownHandle);
    this.startAlarm();
}

Timer.prototype.resetTimer = function () {
    this.remainingTime = (this.hours * 60 * 60 * 1000) +
    (this.minutes * 60 * 1000) +
    (this.seconds * 1000);
    this.renderTimer();
}

Timer.prototype.renderTimer = function () {
    var deltaTime = this.remainingTime;

    var hoursValue = Math.floor(deltaTime / (1000 * 60 * 60));
    deltaTime = deltaTime % (1000 * 60 * 60);

    var minutesValue = Math.floor(deltaTime / (1000 * 60));
    deltaTime = deltaTime % (1000 * 60);

    var secondsValue = Math.floor(deltaTime / (1000));

    this.animateTime(hoursValue, minutesValue, secondsValue);
}

Timer.prototype.animateTime = function (remainingHours, remainingMinutes, remainingSeconds) {
    // position
    $('#hoursValue').css('top', '0em');
    $('#minutesValue').css('top', '0em');
    $('#secondsValue').css('top', '0em');

    $('#hoursNext').css('top', '0em');
    $('#minutesNext').css('top', '0em');
    $('#secondsNext').css('top', '0em');

    var oldHoursString = $('#hoursNext').text();
    var oldMinutesString = $('#minutesNext').text();
    var oldSecondsString = $('#secondsNext').text();

    var hoursString = this.formatTime(remainingHours);
    var minutesString = this.formatTime(remainingMinutes);
    var secondsString = this.formatTime(remainingSeconds);

    $('#hoursValue').text(oldHoursString);
    $('#minutesValue').text(oldMinutesString);
    $('#secondsValue').text(oldSecondsString);

    $('#hoursNext').text(hoursString);
    $('#minutesNext').text(minutesString);
    $('#secondsNext').text(secondsString);

    localStorage.setItem('hours', hoursString);
    localStorage.setItem('minutes', minutesString);
    localStorage.setItem('seconds', secondsString);

    // set and animate
    if (oldHoursString !== hoursString) {
        $('#hoursValue').animate({ top: '-=1em' });
        $('#hoursNext').animate({ top: '-=1em' });
    }

    if (oldMinutesString !== minutesString) {
        $('#minutesValue').animate({ top: '-=1em' });
        $('#minutesNext').animate({ top: '-=1em' });
    }

    if (oldSecondsString !== secondsString) {
        $('#secondsValue').animate({ top: '-=1em' });
        $('#secondsNext').animate({ top: '-=1em' });
    }
}

Timer.prototype.formatTime = function (intergerValue) {
    return intergerValue > 9 ? intergerValue.toString() : '0' + intergerValue.toString();
}

Timer.prototype.decrementTimer = function (that) {
    that.remainingTime -= (1 * 1000);
    
    if (that.remainingTime < 1000) {

        that.stopTimer();
        that.finishTimer();
    }

    that.renderTimer();
}
