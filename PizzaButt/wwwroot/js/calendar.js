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
    eventRender(event, $el) {
        $el.qtip({
            content: {
                communityId: event.communityId,
                cycle: event.cycle,
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
        $el.find('.fc-title').text(function (i, t) {
            return t = event.community.name
        });
        $el.find('.fc-time').text(function (i, t) {
            return t = ""
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

    $('#editEventModalLabel').html('Edit Event');
    $('#editEventModalSave').html('Update Event');
    $('#editCommunityId').val(event.communityId);

    const start = formatDate(event.start);

    editFpStartTime.setDate(start);

    $('#editStartTime').val(start);

    $('#editEventModal').modal('show');
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
    var communityId = [];
    var communityIds = $('#CommunityId').val();
    communityIds.forEach((x) => {
        communityId.push(x.split(",")[0])
    })
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

$('#editEventModalSave').click(() => {
    const communityId = $('#editCommunityId').val();
    const cycle = $('#editCycle').val();
    const startTime = moment($('#editStartTime').val());
    var isNewEvent = false;
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
                        start: value.date,
                        community: value.community,
                        communityId: event.communityId,
                        eventId: key,
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
            "EventId": currentEvent.id,
            "CommunityId": event.communityId,
            "Cycle": event.cycle,
            "Start": event.startTime,
        }
    })
        .then(res => {
            const { message, events } = res.data;

            if (message === '') {
                for (const [key, value] of Object.entries(events)) {
                    const newEvent = {
                        start: value.date,
                        community: value.community,
                        communityId: event.communityId,
                        eventId: key
                    };

                    $('#calendar').fullCalendar('rerenderEvents');
                    $('#calendar').fullCalendar('refetchEvents');
                }
                $('#calendar').fullCalendar('unselect');

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
                "EventId": currentEvent.id,
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

$("#addButton").click(function () {
    console.log($('#CommunityId').val()[0].split(","));
    $('#CommunityId').val().forEach((x, index) => 
    {
        console.log(index);
        var values = $('#CommunityId').val()[index].split(",");
        var comunityName = values[1];
        console.log(values);

        $('#communityList').append('<option value="">' + comunityName + '</option>')
        $('#communityIdList').append('<li value="">' + values[0] + '</li>')
        console.log($('#communityIdList'))
    });

}); 