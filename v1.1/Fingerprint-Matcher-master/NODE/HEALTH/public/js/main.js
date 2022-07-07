{
	class Details {
		constructor() {
			this.DOM = {};

			const detailsTmpl = `
			<div class="details__bg details__bg--down">
				<button class="details__close"><i class="fas fa-2x fa-times icon--cross tm-fa-close"></i></button>
				<div class="details__description"></div>
			</div>						
			`;

			this.DOM.details = document.createElement('div');
			this.DOM.details.className = 'details';
			this.DOM.details.innerHTML = detailsTmpl;
			// DOM.content.appendChild(this.DOM.details);
			document.getElementById('tm-wrap').appendChild(this.DOM.details);
			this.init();
		}
		init() {
			this.DOM.bgDown = this.DOM.details.querySelector('.details__bg--down');
			this.DOM.description = this.DOM.details.querySelector('.details__description');
			this.DOM.close = this.DOM.details.querySelector('.details__close');

			this.initEvents();
		}
		initEvents() {
			// close page when outside of page is clicked.
			document.body.addEventListener('click', () => this.close());
			// prevent close page when inside of page is clicked.
			this.DOM.bgDown.addEventListener('click', function(event) {
							event.stopPropagation();
						});
			// close page when cross button is clicked.
			this.DOM.close.addEventListener('click', () => this.close());
		}
		fill(info) {
			// fill current page info
			this.DOM.description.innerHTML = info.description;
		}		
		getProductDetailsRect(){
			var p = 0;
			var d = 0;

			try {
				p = this.DOM.productBg.getBoundingClientRect();
				d = this.DOM.bgDown.getBoundingClientRect();	
			}
			catch(e){}

			return {
				productBgRect: p,
				detailsBgRect: d
			};
		}
		open(data) {
			if(this.isAnimating) return false;
			this.isAnimating = true;

			this.DOM.details.style.display = 'block';  

			this.DOM.details.classList.add('details--open');

			this.DOM.productBg = data.productBg;

			this.DOM.productBg.style.opacity = 0;

			const rect = this.getProductDetailsRect();

			this.DOM.bgDown.style.transform = `translateX(${rect.productBgRect.left-rect.detailsBgRect.left}px) translateY(${rect.productBgRect.top-rect.detailsBgRect.top}px) scaleX(${rect.productBgRect.width/rect.detailsBgRect.width}) scaleY(${rect.productBgRect.height/rect.detailsBgRect.height})`;
			this.DOM.bgDown.style.opacity = 1;

			$(".treino44").each(function(){
				
				var treinando = $(this).text().replace(/<br>/g , "\n")
				
				$(this).html(treinando)
			})

        
            function getWeekDay(date){
                //Create an array containing each day, starting with Sunday.
                var weekdays = new Array(
                    "Domingo", "Segunda-Feira", "Terça-Feira", "Quarta-Feira", "Quinta-Feira", "Sexta-Feira", "Sábado"
                );
                //Use the getDay() method to get the day.
                var day = date.getDay();
                //Return the element that corresponds to that index.
                return weekdays[day];
            }          
            function my_date_format (d){
                console.log(d);
                d= new Date(d);
                console.log(d);
                var dia = getWeekDay(d);
                var month = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];
                var date = d.getDate() + " " + month[d.getMonth()] + ", " +  d.getFullYear();
                var hora, minuto, segundo;
                if(d.getHours() < 10){hora = '0'+d.getHours();}else{hora = d.getHours();}
                if(d.getMinutes() < 10){minuto = '0'+d.getMinutes();}else{minuto = d.getMinutes();}
                if(d.getSeconds() < 10){segundo = '0'+d.getSeconds();}else{segundo = d.getSeconds();}

                var time = (hora)+":"+minuto+":"+segundo;
               
                return (dia+" "+date); 
            }(new Date());




			$(".vencimento").each(function(){
				var now = new Date(); // Data de hoje
				var past = new Date($(this).text()); // Outra data no passado
				var diff = Math.abs(now.getTime() - past.getTime()); // Subtrai uma data pela outra
				var days = Math.ceil(diff / (1000 * 60 * 60 * 24));

				
				if(new Date($(this).text()) >= new Date()){
					if(days == 1){
						$(this).parent().find(".situacao").html("Vence hoje!");
						$(this).parent().css("color","yellow")
					}
					
	
					if(days > 1){
						$(this).parent().find(".situacao").html(days +" dias para vencer!");
						$(this).parent().css("color","lightgreen")
					}
				}else{
					if(days == 1){
						$(this).parent().find(".situacao").html("Vence hoje!");
						$(this).parent().css("color","yellow")
					}else{
						$(this).parent().find(".situacao").html("Venceu a "+days +" dias!");
						$(this).parent().css("color","red")
					}
					
						
					
				}

				$(this).html(my_date_format($(this).text()))
				
				
			})


			
			
			
		$(".liberaCatraca").click(function(){
			var dialog = bootbox.dialog({
				message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Liberando catraca...</p>',
				closeButton: false
				});

				$.ajax({
					type: 'GET',
					url: '/liberarCatraca',
					data: "",
					success: function (data) {
						// em caso de sucesso...
					},
					error: function (data) {
						// em caso de erro...
					},
					complete: function(){
						setTimeout(()=>{dialog.modal('hide'); },1000);
					}
				});
				
				//also terminated
				
		})


		$(".incluiHorarios").click(function(){
			bootbox.confirm({
				message: '<h4 style="text-align: center; color : black">Insira o novo horário abaixo:</h4><br>'+
				'<input type="text" class="descricao form-control" placeholder="Descrição do horário"><br>'+
				'<label style="color: black">Começa:</label><input style="width: 30%"  type="time" class="comeca form-control" placeholder="Começa">'+
				'<label style="color: black">Termina:</label><input style="width: 30%"  type="time" class="termina form-control" placeholder="Termina"><br>'+
				'<input list="diasDaSemana" type="text" class="diaSemana form-control" placeholder="Dias da Semana"><br>'+
				'<datalist id="diasDaSemana"><option>Seg a Sáb - Normal</option><option>Seg a Sáb - Personalizado</option><option>Domingos e Feriados</option></datalist>',

				buttons: {
					confirm: {
						label: 'Salvar Horário',
						className: 'btn-success'
					},
					cancel: {
						label: 'Cancelar',
						className: 'btn-danger'
					}
				},
				callback: function (result) {
					var descricao = $(".descricao").val();
					var comeca = $(".comeca").val();
					var termina = $(".termina").val();
					var diaSemana = $(".diaSemana").val();


					
			
					
						if(result){
							var dialog = bootbox.dialog({
								message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Salvando...</p>',
								closeButton: false
								});
						
							$.ajax({
								type: 'GET',
								url: '/insereHorario/'+descricao+"/"+comeca+"/"+termina+"/"+diaSemana,
								data: "",
								success: function (data) {
									// em caso de sucesso...
								},
								error: function (data) {
									// em caso de erro...
								},
								complete: function(){
								setTimeout(()=>{dialog.modal('hide');location.reload() },500);
								}
							});


							
					}else{
						setTimeout(()=>{dialog.modal('hide');location.reload() },500);
					}
				}
			});
			
		})

		$(".horario").click(function(){

			var descricao = $(this).find(".descricaoP").text();
			var comeca = $(this).find(".comecaP").text();
			var termina = $(this).find(".terminaP").text();
			var horario = $(this).find(".horarioP").text();
			var ID = $(this).find(".id").text();

			bootbox.confirm({
				message: '<h4 style="text-align: center; color : black">Insira o novo horário abaixo:</h4><br>'+
				'<input value="'+descricao+'" type="text" class="descricao form-control" placeholder="Descrição do horário"><br>'+
				'<label  style="color: black">Começa:</label><input value="'+comeca+'" style="width: 30%"  type="time" class="comeca form-control" placeholder="Começa">'+
				'<label style="color: black">Termina:</label><input value="'+termina+'" style="width: 30%"  type="time" class="termina form-control" placeholder="Termina"><br>'+
				'<input value="'+horario+'" list="diasDaSemana" type="text" class="diaSemana form-control" placeholder="Dias da Semana"><br>'+
				'<datalist id="diasDaSemana"><option>Seg a Sáb - Normal</option><option>Seg a Sáb - Personalizado</option><option>Domingos e Feriados</option></datalist>',

				buttons: {
					confirm: {
						label: 'Salvar Horário',
						className: 'btn-success'
					},
					cancel: {
						label: 'Cancelar',
						className: 'btn-danger'
					}
				},
				callback: function (result) {
					var descricao = $(".descricao").val();
					var comeca = $(".comeca").val();
					var termina = $(".termina").val();
					var diaSemana = $(".diaSemana").val();


					
			
					
						if(result){
							var dialog = bootbox.dialog({
								message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Salvando...</p>',
								closeButton: false
								});
						
							$.ajax({
								type: 'GET',
								url: '/updateHorario/'+ID+'/'+descricao+"/"+comeca+"/"+termina+"/"+diaSemana,
								data: "",
								success: function (data) {
									// em caso de sucesso...
								},
								error: function (data) {
									// em caso de erro...
								},
								complete: function(){
										setTimeout(()=>{dialog.modal('hide');
										location.reload() 
									
										},500);
								}
							});


							
					}else{
						setTimeout(()=>{dialog.modal('hide');location.reload() },500);
					}
				}
			});
			
		})

		 $(".treino").click(function(){
			var nome = $(this).find(".nome").text()
			var treino = $(this).attr("treino").replace(/<br>/g,"\n");
			var numero = $(this).attr("numero");
	
		
			bootbox.confirm({
				message: '<h4 style="text-align: center; color : black">'+nome+'</h4><br>'+
				'<textarea style="width: 100%;" id="treinamento" class="treino"  rows="15">'+treino+'</textarea><br><br><select style="min-height: 50px;" class="form-control" id="tipo"><option>Iniciante</option><option>Normal</option><option>Avançado</option></select>',

				buttons: {
					confirm: {
						label: 'Salvar Treino',
						className: 'btn-success'
					},
					cancel: {
						label: 'Cancelar',
						className: 'btn-danger'
					}
				},
				callback: function (result) {


				//	var dialog = bootbox.dialog({
					//	message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Salvando...</p>',
					//	closeButton: false
					//	});
						if($("#treinamento").val() == ""){$("#treinamento").val("Sem treino..") }
						console.log('/treina/'+numero+'/'+$("#treinamento").val().replace(/\n/g,"<br>"))
					if(result){
						
						$.ajax({
							type: 'GET',
							url: '/treina/'+numero+'/'+$("#treinamento").val().replace(/\n/g,"<br>").replace(/\//g,"-")+'/'+$("#tipo").val(),
							data: "",
							success: function (data) {
							console.log(data);
							//setTimeout(()=>{dialog.modal('hide');location.reload() },500);
							
							},
							error: function (data) {
								// em caso de erro...
							},
							complete: function(){
								// ao final da requisição...
							}
						});


							
					}else{
						setTimeout(()=>{dialog.modal('hide');location.reload() },500);
					}
				}
			});

		 })
		 $(".pesquisaRenova").keyup(function(){
			var TEXTO = $(this).val().toLocaleLowerCase()
			$(".renova").each(function(){
				if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
					$(this).show()					
				}else{
					$(this).hide(0)
				}
			})
		})

		$(".pesquisaInstrutor").keyup(function(){
			var TEXTO = $(this).val().toLocaleLowerCase()
			$(".instrutor").each(function(){
				if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
					$(this).show()					
				}else{
					$(this).hide(0)
				}
			})
		})

		$(".pesquisaNaoInstrutor").keyup(function(){
			var TEXTO = $(this).val().toLocaleLowerCase()
			$(".NaoInstrutor").each(function(){
				if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
					$(this).show()					
				}else{
					$(this).hide(0)
				}
			})
		})


			$(".pesquisaTreino").keyup(function(){
				var TEXTO = $(this).val().toLocaleLowerCase()
				$(".treino").each(function(){
					if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
						$(this).show()					
					}else{
						$(this).hide(0)
					}
				})
			})

			$(".pesquisaAtivo").keyup(function(){
				var TEXTO = $(this).val().toLocaleLowerCase()
				$(".ativo").each(function(){
					if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
						$(this).show()					
					}else{
						$(this).hide(0)
					}
				})
			})

			




		
			
			/*
			$(".incluiHorario").click(function(){
				var TR = "<tr class='horario'>"+
				"<td class='pt-3-half one numero' ></td>"+
                "<td class='pt-3-half one nome' ></td>"+
                "<td class='pt-3-half one situacao' ></td>"+
                 "<td class='pt-3-half number vencimento' ></td><td><button class='salvaHorario'>SALVAR</button></tr>"+
				$("#conteudoHorario").append(TR);

				$(".salvaHorario").click(function(){
					var dialog = bootbox.dialog({
					message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i>Salvando...</p>',
					closeButton: false
					});

					//also terminated
					setTimeout(()=>{dialog.modal('hide'); },1000)
					$(this).parent().remove();
				})
			})
*/

			$(".renova").click(function(){
				var nome = $(this).find(".nome").text()
				var data = $(this).find(".vencimento").text()
				var numero = $(this).find(".numero").text()

									bootbox.prompt({
										title: '<p style="color: red" class="text-center mb-0"><i class="fa fa-user fa-cog"></i> - Entre com a senha de administrador!</p>',
										inputType: 'password',
										callback: function (result) {
											if(result == "123456"){
											


															bootbox.prompt({
																title: "<p style='color:black'>Deseja renovar a situação de "+nome+" até que dia?</p>",
																color: "black",
														
																value: data,
																inputType: 'date',
																callback: function (result) {

																	
																				var dialog = bootbox.dialog({
																					message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Renovando até '+result+'..</p>',
																					closeButton: false
																				});
																				console.log("tentando "+numero)
														
														
																				if(result != null ){
																					$.ajax({
																						type: 'GET',
																						url: '/renova/'+numero+'/'+result,
																						data: "",
																						success: function (data) {
																							console.log(data);
																							setTimeout(()=>{dialog.modal('hide');location.reload() },1000);
																							
																							
																						
														
																						},
																						error: function (data) {
																							// em caso de erro...
																						},
																						complete: function(){
																							console.log("acabei")
																						}
																					});
																				}else{
																					setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																				}

																			
																	
																}
															});

												}else{
													bootbox.alert("<p style='color: red'>Senha incorreta!</p>")
												}
											}
										});
							})

							$(".instrutor").click(function(){
								var nome = $(this).find(".nome").text()
					
								var numero = $(this).find(".numero").text()
				
													bootbox.prompt({
														title: '<p style="color: red" class="text-center mb-0"><i class="fa fa-user fa-cog"></i> - Entre com a senha de administrador!</p>',
														inputType: 'password',
														callback: function (result) {
															if(result == "123456"){
															
				
				
																			bootbox.confirm({
																				title: "<p style='color:black'>Deseja remover  "+nome+" da lista de instrutores?</p>",
																				message: "....",
																				color: "black",
																				buttons: {
																					confirm: {
																						label: 'SIM',
																						className: 'btn-success'
																					},
																					cancel: {
																						label: 'NÃO',
																						className: 'btn-danger'
																					}
																				},
																				callback: function (result) {
																								if(result){
																									var dialog = bootbox.dialog({
																										message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Removendo...</p>',
																										closeButton: false
																									});
																									console.log("tentando "+numero)
																			
																			
																									if(result != null ){
																										$.ajax({
																											type: 'GET',
																											url: '/removeInstrutor/'+numero,
																											data: "",
																											success: function (data) {
																												console.log(data);
																												setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																												
																												
																											
																			
																											},
																											error: function (data) {
																												// em caso de erro...
																											},
																											complete: function(){
																												console.log("acabei")
																											}
																										});
																									}else{
																										setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																									}
																								}
																					
																								
				
																							
																					
																				}
																			});
				
																}else{
																	bootbox.alert("<p style='color: red'>Senha incorreta!</p>")
																}
															}
														});
											})
											$(".NaoInstrutor").click(function(){
												var nome = $(this).find(".nome").text()
									
												var numero = $(this).find(".numero").text()
								
																	bootbox.prompt({
																		title: '<p style="color: red" class="text-center mb-0"><i class="fa fa-user fa-cog"></i> - Entre com a senha de administrador!</p>',
																		message: "....",
																		inputType: 'password',
																		callback: function (result) {
																			if(result == "123456"){
																			
								
								
																							bootbox.confirm({
																								title: "<p style='color:black'>Deseja adicionar  "+nome+" na lista de instrutores?</p>",
																								message:"...",
																								color: "black",
																								buttons: {
																									confirm: {
																										label: 'SIM',
																										className: 'btn-success'
																									},
																									cancel: {
																										label: 'NÃO',
																										className: 'btn-danger'
																									}
																								},
																								callback: function (result) {
																												if(result){
																													var dialog = bootbox.dialog({
																														message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Adicionando...</p>',
																														closeButton: false
																													});
																													console.log("tentando "+numero)
																							
																							
																													if(result != null ){
																														$.ajax({
																															type: 'GET',
																															url: '/adicionaInstrutor/'+numero,
																															data: "",
																															success: function (data) {
																																console.log(data);
																																setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																																
																																
																															
																							
																															},
																															error: function (data) {
																																// em caso de erro...
																															},
																															complete: function(){
																																console.log("acabei")
																															}
																														});
																													}else{
																														setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																													}
																												}
																									
																												
								
																											
																									
																								}
																							});
								
																				}else{
																					bootbox.alert("<p style='color: red'>Senha incorreta!</p>")
																				}
																			}
																		});
															})
				


							$(".excluir").click(function(){
								var nome = $(this).find(".nome").text()
								var data = $(this).find(".vencimento").text()
								var numero = $(this).find(".numero").text()
				
													bootbox.prompt({
														title: '<p style="color: red" class="text-center mb-0"><i class="fa fa-user fa-cog"></i> - Entre com a senha de administrador!</p>',
														inputType: 'password',
														callback: function (result) {
															if(result == "123456"){
															
				
				
																			bootbox.confirm({
															
																				message: "<p style='color:black'>Deseja mesmo excluir o(a) aluno(a)<br>"+nome+"?</p>",
																				buttons: {
																					confirm: {
																						label: 'SIM',
																						className: 'btn-success'
																					},
																					cancel: {
																						label: 'NÃO',
																						className: 'btn-danger'
																					}
																				},
																				callback: function (result) {
																					
																					if(result){
																						var dialog = bootbox.dialog({
																							message: '<p style="color: black" class="text-center mb-0"><i class="fa fa-spin fa-cog"></i> Excluindo..</p>',
																							closeButton: false
																						});
																						console.log("tentando "+numero)
																
																
																						if(result != null ){
																							$.ajax({
																								type: 'GET',
																								url: '/exclui/'+numero,
																								data: "",
																								success: function (data) {
																									console.log(data);
																									setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																									
																									
																								
																
																								},
																								error: function (data) {
																									// em caso de erro...
																								},
																								complete: function(){
																									console.log("acabei")
																								}
																							});
																						}else{
																							setTimeout(()=>{dialog.modal('hide');location.reload() },500);
																						}
																					}
																								
				
																							
																					
																				}
																			});
				
																}else{
																	bootbox.alert("<p style='color: red'>Senha incorreta!</p>")
																}
															}
														});

														
											})

		



            // animate background
            anime({
                targets: [this.DOM.bgDown],
                duration: (target, index) => index ? 800 : 250,
                easing: (target, index) => index ? 'easeOutElastic' : 'easeOutSine',
                elasticity: 250,
                translateX: 0,
                translateY: 0,
                scaleX: 1,
                scaleY: 1,                              
                complete: () => this.isAnimating = false
            });

            // animate content
            anime({
                targets: [this.DOM.description],
                duration: 1000,
                easing: 'easeOutExpo',                
                translateY: ['100%',0],
                opacity: 1
            });

            // animate close button
            anime({
                targets: this.DOM.close,
                duration: 250,
                easing: 'easeOutSine',
                translateY: ['100%',0],
                opacity: 1
            });

            this.setCarousel();

            window.addEventListener("resize", this.setCarousel);
		}
		close() {
			if(this.isAnimating) return false;
			this.isAnimating = true;

			this.DOM.details.classList.remove('details--open');

			anime({
                targets: this.DOM.close,
                duration: 250,
                easing: 'easeOutSine',
                translateY: '100%',
                opacity: 0
            });

            anime({
                targets: [this.DOM.description],
                duration: 20,
                easing: 'linear',
                opacity: 0
            });

            const rect = this.getProductDetailsRect();
            anime({
                targets: [this.DOM.bgDown],
                duration: 250,
                easing: 'easeOutSine',                
                translateX: (target, index) => {
                    return index ? rect.productImgRect.left-rect.detailsImgRect.left : rect.productBgRect.left-rect.detailsBgRect.left;
                },
                translateY: (target, index) => {
                    return index ? rect.productImgRect.top-rect.detailsImgRect.top : rect.productBgRect.top-rect.detailsBgRect.top;
                },
                scaleX: (target, index) => {
                    return index ? rect.productImgRect.width/rect.detailsImgRect.width : rect.productBgRect.width/rect.detailsBgRect.width;
                },
                scaleY: (target, index) => {
                    return index ? rect.productImgRect.height/rect.detailsImgRect.height : rect.productBgRect.height/rect.detailsBgRect.height;
                },
                complete: () => {
                    this.DOM.bgDown.style.opacity = 0;
					this.DOM.bgDown.style.transform = 'none';
					try{
						this.DOM.productBg.style.opacity = 1;
						
					}catch{
					
						

						
						$(".pesquisaTreinoCard").keyup(function(){
							var TEXTO = $(this).val().toLocaleLowerCase()
							console.log("pesquisando ..."+TEXTO)
							
							$(".treinoAluno").each(function(){
								console.log(TEXTO +" "+$(this).text().toLocaleLowerCase())
								if($(this).text().toLocaleLowerCase().indexOf(TEXTO) > -1 || TEXTO == ""){
									
									$(this).show()					
								}else{
									$(this).hide(0)
								}
							})
						});

								

			
			
					}
                    
                    this.DOM.details.style.display = 'none';                    
                    this.isAnimating = false;
                }
            });
		}
		// Slick Carousel
        setCarousel() {
          
	        var slider = $('.details .tm-img-slider');

	        if(slider.length) { // check if slider exist

		        if (slider.hasClass('slick-initialized')) {
		            slider.slick('destroy');
		        }

		        if($(window).width() > 767){
		            // Slick carousel
		            slider.slick({
		                dots: true,
		                infinite: true,
		                slidesToShow: 4,
		                slidesToScroll: 3
		            });
		        }
		        else {
		            slider.slick({
			            dots: true,
			            infinite: true,
			            slidesToShow: 2,
			            slidesToScroll: 1
		        	});
		     	}	
	        }          
        }
	}; // class Details

	class Item {
		constructor(el) {
			this.DOM = {};
			this.DOM.el = el;
			this.DOM.product = this.DOM.el.querySelector('.product');
			this.DOM.productBg = this.DOM.product.querySelector('.product__bg');

			this.info = {
				description: this.DOM.product.querySelector('.product__description').innerHTML
			};

			this.initEvents();
		}
		initEvents() {
			this.DOM.product.addEventListener('click', () => this.open());
		}
		open() {
			DOM.details.fill(this.info);
			DOM.details.open({
				productBg: this.DOM.productBg
			});
		}
	}; // class Item

	const DOM = {};
	DOM.grid = document.querySelector('.grid');
	DOM.content = DOM.grid.parentNode;
	DOM.gridItems = Array.from(DOM.grid.querySelectorAll('.grid__item'));
	let items = [];
	DOM.gridItems.forEach(item => items.push(new Item(item)));

	DOM.details = new Details();





				
	
};




