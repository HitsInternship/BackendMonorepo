using System.Web.Mvc;
using DocumentModule.Contracts.Queries;
using DocumentModule.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using FileContentResult = Microsoft.AspNetCore.Mvc.FileContentResult;

namespace DocumentModule.Controllers.Controllers
{
    [ApiController]
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/documents/")]
    public class DocumentController : ControllerBase
    {
        private readonly ISender _sender;
        public DocumentController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Получает название документа по идентификатору.
        /// </summary>
        /// <returns>Название документа.</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{documentId}/name")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentName(Guid documentId, DocumentType documentType)
        {
            return Ok(await _sender.Send(new GetDocumentNameQuery(documentId, documentType)));
        }

        /// <summary>
        /// Получает документ по идентификатору.
        /// </summary>
        /// <returns>Документ.</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{documentId}")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocument(Guid documentId, DocumentType documentType)
        {
            return await _sender.Send(new GetDocumentQuery(documentId, documentType));
        }
    }
}
