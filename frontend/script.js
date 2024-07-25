$(document).ready(function() {
    let timeEntryID = null;

    // Function to handle starting the time entry
    $('#startServiceBtn').on('click', function() {
        let timeEntryData = {
            companyID: $('#companyID').val(),
            serviceID: $('#serviceID').val(),
            clientID: $('#clientID').val(),
            user: $('#user').val(),
            notes: $('#notes').val(),
            comment: $('#comment').val()
        };

        $.ajax({
            type: 'POST',
            url: '/api/timeentry/start',
            data: JSON.stringify(timeEntryData),
            contentType: 'application/json',
            success: function(response) {
                alert(response.Message);
                timeEntryID = response.TimeEntryID;
            },
            error: function(error) {
                alert('Error starting time entry.');
                console.error(error);
            }
        });
    });

    // Function to handle ending the time entry
    $('#endServiceBtn').on('click', function() {
        if (timeEntryID) {
            $.ajax({
                type: 'POST',
                url: `/api/timeentry/end/${timeEntryID}`,
                success: function(response) {
                    alert(response.Message);
                    timeEntryID = null;
                },
                error: function(error) {
                    alert('Error ending time entry.');
                    console.error(error);
                }
            });
        } else {
            alert('No active time entry to end.');
        }
    });
});
