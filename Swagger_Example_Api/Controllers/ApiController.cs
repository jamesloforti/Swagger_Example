// ******************************************************************************************************************
//  Swagger_Example - simple example to demonstrate swagger UI.
//  Copyright(C) 2019  James LoForti
//  Contact Info: jamesloforti@gmail.com
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.If not, see<https://www.gnu.org/licenses/>.
//									     ____.           .____             _____  _______   
//									    |    |           |    |    ____   /  |  | \   _  \  
//									    |    |   ______  |    |   /  _ \ /   |  |_/  /_\  \ 
//									/\__|    |  /_____/  |    |__(  <_> )    ^   /\  \_/   \
//									\________|           |_______ \____/\____   |  \_____  /
//									                             \/          |__|        \/ 
//
// ******************************************************************************************************************
//
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swagger_Example_Api.Model;
using System;

namespace Swagger_Example_Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ApiController : ControllerBase
	{
		//Add logger

		public ApiController()
		{

		}

		[HttpGet]
		public IActionResult Get()
		{
			//Add logging

			try
			{
				//Do something...
				return Ok("GET successful!");
			}
			catch (Exception ex)
			{
				//Add logging
				return BadRequest($"GET failed! Exception: {JsonConvert.SerializeObject(ex)}");
			}
		}

		[HttpPost]
		public IActionResult Post([FromBody]GenericRequest request)
		{
			//Add logging

			try
			{
				//Do something...
				return Ok("POST successful!");
			}
			catch (Exception ex)
			{
				//Add logging
				return BadRequest($"POST failed using request: {JsonConvert.SerializeObject(request)}! " +
					$"Exception: {JsonConvert.SerializeObject(ex)}");
			}
		}
	}
}
