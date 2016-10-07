function updatePanel(e)
{
    var dataItem = this.dataItem(e.item.index());

    if (dataItem.Value == 0)
    {
        $('#update-panel-certificate').hide();

        $('#send-mail-button-panel').show();
        $('#test-button-panel').hide();
    }
    else
    {
        $('#update-panel-certificate').show();

        $('#send-mail-button-panel').hide();
    }

    console.log($('#certificateTime'));

    $('#certificateTime').change();
}

function updatePanelTest(e)
{
    var dataItem = this.dataItem(e.item.index());

    if (dataItem.Value == 0)
    {
        $('#update-panel-no-test').hide();
        $('#update-panel-test').hide();

        $('#send-mail-button-panel').hide();
        $('#test-button-panel').hide();
    }
    else if (dataItem.Value == 1)
    {
        $('#update-panel-no-test').show();
        $('#update-panel-test').hide();

        $('#send-mail-button-panel').show();
        $('#test-button-panel').hide();
    }
    else
    {
        $('#update-panel-no-test').hide();
        $('#update-panel-test').show();

        $('#send-mail-button-panel').hide();
        $('#test-button-panel').show();
    }
}