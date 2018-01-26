using System.Collections.Generic;

[System.Serializable]
public class MadlibData {

    public List<OptionData> options;

    public MadlibData(List<OptionData> options) {
        this.options = options;
    }

}
