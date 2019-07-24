let currentEvent;
const formatDate = date => date === null ? '' : moment(date).format("MM/DD/YYYY");
const fpStartTime = flatpickr("#StartTime", {
    dateFormat: "m/d/Y"
});

const editFpStartTime = flatpickr("#editStartTime", {
    dateFormat: "m/d/Y"
});

$('#calendar').fullCalendar({
    themeSystem: 'bootstrap4',
    defaultView: 'month',
    height: 'parent',
    header: {
        left: 'prev,next today',
        center: 'communityId',
        right: 'month,agendaWeek,agendaDay'
    },
    events: '/Home/GetCalendarEvents',

    eventRender: function eventRender(event, element, view) {
        element.find('.fc-title').text(function (i, t) {
            return t = event.community.name
        });
        element.find('.fc-time').text(function (i, t) {
            return t = ""
        });
        return ['all', event.communityId.toString()].indexOf($('#communitySelector').val()) >= 0
    }
});

/**
 * Calendar Methods
 **/

$('#communitySelector').on('change', function () {
    $('#calendar').fullCalendar('rerenderEvents');
})