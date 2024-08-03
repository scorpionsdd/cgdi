Select 	documento_id, 
		volante, 
		remitente_id, 
		remitente_area_id, 
		tipo_remitente, 
		tipo_documento_id, 
		fecha_documento_fuente, 
		fecha_elaboracion, 
		fecha_envio, 
		referencia, 
--		asunto, 
--		anexo, 
--		resumen, 
--		instruccion, 
		usuario_id, 
		documento_bis_id, 
		requiere_tramite, 
--		nombre, area, puesto, 
		tipo_tramite,
		documento_turnar_id, 
		status, 
		destinatario_id, 
		destinatario_area_id, 
		substr(nombre_destinatario,1,20), 
		substr(destinatario_area,1,20) 

From 	(select	sof_documento.documento_id,
				sof_documento.volante,
				sof_documento.remitente_id,
				sof_documento.remitente_area_id,
				tipo_remitente,
				sof_documento.tipo_documento_id,
				sof_documento.fecha_documento_fuente,
				sof_documento.fecha_elaboracion,
				sof_documento.fecha_envio,
				sof_documento.referencia,
				sof_documento.asunto,
				sof_documento.anexo,
				sof_documento.resumen,
				sof_documento_turnar.instruccion,
				sof_documento.usuario_id,
				sof_documento.documento_bis_id,
				sof_documento.requiere_tramite,
				
				sof_remitente_interno_titular.nombre,
				sof_remitente_interno_titular.puesto,
				sof_remitente_interno_area.area,
				
				sof_tipo_tramite.tipo_tramite, 
				sof_documento_turnar.documento_turnar_id,
				decode(sof_documento_turnar.estatus,'0','Nuevo','1','Tramite','2','Returnado','Concluido') status, 
				sof_documento_turnar.destinatario_id, 
				sof_documento_turnar.destinatario_area_id,

				sof_destinatario_titular.nombre nombre_destinatario, 
				sof_destinatario_area.area destinatario_area

 		From 	sof_documento_turnar, 
				sof_documento, 
				sof_tipo_documento, 
				hint_v_usuarios sof_remitente_interno_area,  
				hint_v_usuarios sof_remitente_interno_titular, 
				sof_tipo_tramite,
				hint_v_usuarios sof_destinatario_area , 
				hint_v_usuarios sof_destinatario_titular 

		Where 	sof_documento_turnar.destinatario_id in (79154) 
		and 	sof_documento.documento_id 					= sof_documento_turnar.documento_id 
		and 	sof_documento.tipo_remitente				= 'I'
		and 	sof_destinatario_area.clave_area 			= sof_documento_turnar.destinatario_area_id
		and 	sof_destinatario_titular.id_usuario 		= sof_documento_turnar.destinatario_id 
		and 	sof_tipo_tramite.tipo_tramite_id 			= sof_documento_turnar.tipo_tramite_id  
		and 	sof_tipo_documento.tipo_documento_id 		= sof_documento.tipo_documento_id 
		and 	sof_remitente_interno_area.clave_area 		= sof_documento.remitente_area_id  
		and 	sof_remitente_interno_titular.id_usuario 	= sof_documento.remitente_id)  
		
		into 
		select	sof_documento.documento_id,
				sof_documento.volante,
				sof_documento.remitente_id,
				sof_documento.remitente_area_id,
				tipo_remitente,
				sof_documento.tipo_documento_id,
				sof_documento.fecha_documento_fuente,
				sof_documento.fecha_elaboracion,
				sof_documento.fecha_envio,
				sof_documento.referencia,
				sof_documento.asunto,
				sof_documento.anexo,
				sof_documento.resumen,
				sof_documento_turnar.instruccion,
				sof_documento.usuario_id,
				sof_documento.documento_bis_id,
				sof_documento.requiere_tramite,
				decode(sof_documento.tipo_remitente,'I', sof_remitente_interno_titular.nombre, sof_remitente_externo_titular.nombre) nombre,
				decode(sof_documento.tipo_remitente,'I', sof_remitente_interno.area, sof_remitente_externo.dependencia) area, 
				decode(sof_documento.tipo_remitente,'I', sof_remitente_interno_titular.puesto, sof_remitente_externo_titular.puesto) puesto, 
				sof_tipo_tramite.tipo_tramite, 
				sof_documento_turnar.documento_turnar_id,
				decode(sof_documento_turnar.estatus,'0','Nuevo','1','Tramite','2','Returnado','Concluido') status, 
				sof_documento_turnar.destinatario_id, 
				sof_documento_turnar.destinatario_area_id,
				sof_destinatario_titular.nombre nombre_destinatario, 
				sof_destinatario_area.area destinatario_area
 		From 	sof_documento_turnar, 
				sof_documento, 
				sof_tipo_documento, 
				hint_v_usuarios sof_remitente_interno_titular, 
				(select distinct clave_area, area from hint_v_usuarios where area is not null) sof_remitente_interno,  
				sof_tipo_tramite,
				sof_remitente_externo, 
				sof_remitente_externo_titular, 
				(select distinct clave_area, area from hint_v_usuarios where area is not null) sof_destinatario_area , 
				hint_v_usuarios sof_destinatario_titular 

		Where 	sof_documento_turnar.destinatario_id in (79154) 
		and 	sof_destinatario_area.clave_area = sof_documento_turnar.destinatario_area_id
		and 	sof_destinatario_titular.id_usuario = sof_documento_turnar.destinatario_id 
		and 	sof_tipo_tramite.tipo_tramite_id = sof_documento_turnar.tipo_tramite_id  
		and 	sof_documento.documento_id = sof_documento_turnar.documento_id 
		and 	sof_tipo_documento.tipo_documento_id = sof_documento.tipo_documento_id 
		and 	sof_remitente_interno.clave_area(+) = sof_documento.remitente_area_id  
		and 	sof_remitente_interno_titular.id_usuario(+) = sof_documento.remitente_id  
		and 	sof_remitente_externo.remitente_externo_id(+) = sof_documento.remitente_area_id 
		and 	sof_remitente_externo_titular.remitente_externo_titular_id(+) = sof_documento.remitente_id	 
	 )  
order by destinatario_area_id, volante
/
