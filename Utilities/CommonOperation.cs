namespace SampleTaskApp.Utilities
{
    public class CommonOperation
    {
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public int Total { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }

        public CommonOperation()
        {
            Message = string.Empty;
            Data = (dynamic)null;
            Total =0;
            Type = 0;
            Status = 200;
        }

        public static string CommonDeleteMessage = "Data has been deleted successfully";
        public static string CommonErrorMessage = "Ops, An error occurred.";
        public static string CommonSaveMessage = "Data has been saved successfully";
        public static string CommonUpdateMessage = "Data has been updated successfully";

        public static CommonOperation SETCommonOperation(int nDBOperation, dynamic data = null)
        {
            var sMsg = new CommonOperation();

            if (nDBOperation == 1) sMsg.Message = CommonSaveMessage;
            else if (nDBOperation == 2) sMsg.Message = CommonUpdateMessage;
            else if (nDBOperation == 3) sMsg.Message = CommonDeleteMessage;
            else if (nDBOperation == 5) sMsg.Message = CommonErrorMessage;
            if (data != null)
            {
                sMsg.Data = data;
            }
            sMsg.Type = nDBOperation;
            return sMsg;
        }
    }
}
