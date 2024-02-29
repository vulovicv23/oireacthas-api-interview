namespace OireachtasAPI.Models
{
    public class LoadDataResult<TResponseModel>
    {
        public LoadDataResult(BaseResponseModel<TResponseModel> responseModel, bool useLocalFile)
        {
            ResponseModel = responseModel;
            UseLocalFile = useLocalFile;
        }

        public BaseResponseModel<TResponseModel> ResponseModel { get; }

        public bool UseLocalFile { get; }
    }
}