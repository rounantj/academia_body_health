

const Sequelize = require('sequelize')



const sequelize = new Sequelize("fingers","root","", {
    host:"localhost",
    dialect:'mysql'
})
sequelize.authenticate().then( function(){
console.log("conectou")
}).catch(function(erro){
    console.log("deu erro:"+ erro)
})







module.exports = {    
    Sequelize:Sequelize,
    sequelize: sequelize
}