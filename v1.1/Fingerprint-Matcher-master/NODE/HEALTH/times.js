
const db = require('./db')
const Times = db.sequelize.define('horario',{
    descricao:{
        type:db.Sequelize.STRING
    },
    comeca:{
        type:db.Sequelize.STRING
    },
    termina:{
        type:db.Sequelize.STRING
    },
    horario:{
        type:db.Sequelize.STRING
    }
})







module.exports = Times