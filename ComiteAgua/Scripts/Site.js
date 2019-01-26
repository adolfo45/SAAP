function BloquearProcesando() {
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: 0.50,
            color: '#fff'
        },
        message: 'Procesando <i class="fa fa-spinner fa-pulse"></i>'
    });
} // function BloquearProcesando()

function BloquearCargando() {
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: 0.50,
            color: '#fff'
        },
        message: 'Cargando <i class="fa fa-spinner fa-pulse"></i>'
    });
} // function BloquearCargando() 

function Desbloquear() {
    $.unblockUI();

} // function Desbloquear()