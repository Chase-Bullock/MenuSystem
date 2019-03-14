let currentEvent;
const formatDate = date => date === null ? '' : moment(date).format("MM/DD/YYYY");
const fpStartTime = flatpickr("#StartTime", {
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
    eventRender(event, $el) {
        $el.qtip({
            content: {
                communityId: event.communityId,
                text: event.cycle
            },
            hide: {
                event: 'unfocus'
            },
            show: {
                solo: true
            },
            position: {
                my: 'top left',
                at: 'bottom left',
                viewport: $('#calendar-wrapper'),
                adjust: {
                    method: 'shift'
                }
            }
        });
    },
    events: '/Home/GetCalendarEvents',
    eventClick: updateEvent,
    selectable: true,
    select: addEvent
});

/**
 * Calendar Methods
 **/

function updateEvent(event, element) {
    currentEvent = event;

    if ($(this).data("qtip")) $(this).qtip("hide");

    $('#eventModalLabel').html('Edit Event');
    $('#eventModalSave').html('Update Event');
    $('#CommunityId').val(event.communityId);
    $('#Cycle').val(event.cycle);
    $('#isNewEvent').val(false);

    const start = formatDate(event.start);

    fpStartTime.setDate(start);

    $('#StartTime').val(start);

    $('#eventModal').modal('show');
}

function addEvent(start) {
    $('#eventForm')[0].reset();

    $('#eventModalLabel').html('Add Event');
    $('#eventModalSave').html('Create Event');
    $('#isNewEvent').val(true);

    start = formatDate(start);

    fpStartTime.setDate(start);

    $('#eventModal').modal('show');
}

/**
 * Modal
 * */

$('#eventModalSave').click(() => {
    const communityId = $('#CommunityId').val();
    const cycle = $('#Cycle').val();
    const startTime = moment($('#StartTime').val());
    const isNewEvent = $('#isNewEvent').val() === 'true' ? true : false;

    if (!startTime.isValid()) {
        alert('Please enter Start Time');
        return;
    }

    const event = {
        communityId,
        cycle,
        startTime: startTime._i,
    };

    if (isNewEvent) {
        sendAddEvent(event);
    } else {
        sendUpdateEvent(event);
    }
});

function sendAddEvent(event) {
    axios({
        method: 'post',
        url: '/Home/AddEvent',
        data: {
            "CommunityId": event.communityId,
            "Cycle": event.cycle,
            "Start": event.startTime,
        }
    })
        .then(res => {
            const { message, events } = res.data;

            if (message === '') {
                for (const [key, value] of Object.entries(events))
                {
                    const newEvent = {
                        start: value,
                        communityId: event.communityId,
                        eventId: key
                    };

                    $('#calendar').fullCalendar('renderEvent', newEvent);
                }
                $('#calendar').fullCalendar('unselect');

                $('#eventModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

function sendUpdateEvent(event) {
    axios({
        method: 'post',
        url: '/Home/UpdateEvent',
        data: {
            "EventId": currentEvent.eventId,
            "CommunityId": event.communityId,
            "Cycle": event.cycle,
            "Start": event.startTime,
        }
    })
        .then(res => {
            const { message } = res.data;

            if (message === '') {
                currentEvent.communityId = event.communityId;
                currentEvent.cycle = event.cycle;
                currentEvent.start = event.startTime;

                $('#calendar').fullCalendar('updateEvent', currentEvent);
                $('#eventModal').modal('hide');
            } else {
                alert(`Something went wrong: ${message}`);
            }
        })
        .catch(err => alert(`Something went wrong: ${err}`));
}

$('#deleteEvent').click(() => {
    if (confirm(`Do you really want to delete "${currentEvent.communityId}" event?`)) {
        axios({
            method: 'post',
            url: '/Home/DeleteEvent',
            data: {
                "EventId": currentEvent.eventId
            }
        })
            .then(res => {
                const { message } = res.data;

                if (message === '') {
                    $('#calendar').fullCalendar('removeEvents', currentEvent._id);
                    $('#eventModal').modal('hide');
                } else {
                    alert(`Something went wrong: ${message}`);
                }
            })
            .catch(err => alert(`Something went wrong: ${err}`));
    }
});