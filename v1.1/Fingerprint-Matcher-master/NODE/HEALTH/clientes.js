
const db = require('./db')
const Clientes = db.sequelize.define('cliente',{
    num_identificador:{
        type:db.Sequelize.STRING
    },
    nome:{
        type:db.Sequelize.STRING
    }
,
    telefone:{
        type:db.Sequelize.STRING
    },
    sexo:{
        type:db.Sequelize.STRING
    },
    email:{
        type:db.Sequelize.STRING
    },
    cpf:{
        type:db.Sequelize.STRING
    },
    dataVencimento:{
        type:db.Sequelize.DATE
    },
    horario:{
        type:db.Sequelize.STRING
    },
    treinos:{
        type:db.Sequelize.STRING
    },
    tipoTreino:{
        type:db.Sequelize.STRING
    }
})

function apaga(valor){
    Formularios.destroy({where:{'id':valor}});
}





module.exports = Clientes