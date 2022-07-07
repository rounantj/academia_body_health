var url_atual = window.location.href;

			if(url_atual.split("/")[1] == "treinamentos"){
				bootbox.prompt({
					title: "<p style='color: black'>Selecione o seu nome!</p>",
					inputType: 'select',
					multiple: true,
					value: ['1','3'],
					inputOptions: [
					{
						text: 'Choose one...',
						value: '',
					},
					{
						text: 'Choice One',
						value: '1',
					},
					{
						text: 'Choice Two',
						value: '2',
					},
					{
						text: 'Choice Three',
						value: '3',
					}
					],
					callback: function (result) {
						console.log(result);
					}
				});
			}else{
				bootbox.prompt({
					title: "<p style='color: black'>errou</p>",
					inputType: 'select',
					multiple: true,
					value: ['1','3'],
					inputOptions: [
					{
						text: 'Choose one...',
						value: '',
					},
					{
						text: 'Choice One',
						value: '1',
					},
					{
						text: 'Choice Two',
						value: '2',
					},
					{
						text: 'Choice Three',
						value: '3',
					}
					],
					callback: function (result) {
						console.log(result);
					}
				});
			}