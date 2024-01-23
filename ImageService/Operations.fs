namespace BeachMobile.ImageService

open System.Threading.Tasks
open Language

module Operations =

    type Get           = ImageRequest          -> Task<Result<Image, ErrorDescription>>
    type GetByCategory = CategoryImagesRequest -> Task<Result<Image, ErrorDescription>>
    type GetAll        = AllImagesRequest      -> Task<Result<Image, ErrorDescription>>

    type Add = AddRequest -> Task<Result<unit, ErrorDescription>>