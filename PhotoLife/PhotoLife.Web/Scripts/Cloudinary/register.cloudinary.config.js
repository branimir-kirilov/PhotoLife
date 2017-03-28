$.cloudinary.config("cloud_name", "djga3zgwr");
$(function () {
    if ($.fn.cloudinary_fileupload !== undefined) {
        $("input.cloudinary-fileupload[type=file]").cloudinary_fileupload();
    }
    
    $('#submit-btn').hide();
    function prettydump(obj) {
        ret = ""
        $.each(obj, function (key, value) {
            ret += "<tr><td>" + key + "</td><td>" + value + "</td></tr>";
        });
        return ret;
    }

    $('.cloudinary-fileupload')
        .fileupload({
            dropZone: '#direct_upload',
            start: function () {
                $('.status_value').text('Starting direct upload...');
            },
            progress: function () {
                $('.status_value').text('Uploading...');
            },
        })
        .on('cloudinarydone',
            function (e, data) {
                $('.status_value').text('Selected Picture');
                $('.upload-box').hide();
                $('#submit-btn').show();

                $.post('/Account/UploadDirect', data.result);
                var info = $('<div class="uploaded_info"/>');
                console.log(data.result);

                $("#pic-url").val(data.result.url);

                $(info)
                    .append($('<div class="image"/>')
                        .append(
                            $.cloudinary.image(data.result.public_id,
                            {
                                format: data.result.format,
                                width: 200,
                                height: 200,
                                crop: "fill"
                            })
                        ));
                $('.uploaded_info_holder').append(info);
            });
});