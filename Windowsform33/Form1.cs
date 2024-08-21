using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using weka.core.converters;
using System.Collections;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Windowsform33
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        OpenFileDialog ofd = new OpenFileDialog();
        String fileDirectory = "";
        double max = -9999999;
        int count = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "ARFF|*.arff";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.SafeFileName;
                fileDirectory = ofd.FileName;
            }
        }

        static weka.classifiers.Classifier cl_Naive = null;
        static weka.classifiers.Classifier cl_Knn = null;
        static weka.classifiers.Classifier cl_Tree = null;
        static weka.classifiers.Classifier cl_NN = null;
        static weka.classifiers.Classifier cl_SVM = null;
        const int percentSplit = 66;

        private void result_Click(object sender, EventArgs e)
        {
            ArrayList algorithms = new ArrayList();
            algorithms.Add("Naive Bayes");
            algorithms.Add("K Nearest Neighbor");
            algorithms.Add("Decision Tree");
            algorithms.Add("Neural Network");
            algorithms.Add("Support Vector Machine");
            ArrayList successPercent = new ArrayList();
            double res_Naive, res_KNN, res_NN, res_Tree, res_SVM = 0.0;
            string nameOfAlgo = "";

            // NAIVE BAYES ALGORITHM
            weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(fileDirectory));
            insts.setClassIndex(insts.numAttributes() - 1);

            // 10-fold cross-validation
            weka.classifiers.bayes.NaiveBayes cl_Naive = new weka.classifiers.bayes.NaiveBayes(); // sınıflandırıcıyı burada oluşturun
            weka.classifiers.Evaluation eval = new weka.classifiers.Evaluation(insts);
            eval.crossValidateModel(cl_Naive, insts, 10, new java.util.Random(1));

            res_Naive = eval.pctCorrect();

            // DataGridView için gerekli ayarlamaları yapın
            dataGridView1.ColumnCount = 2;
            dataGridView1.RowCount = insts.numAttributes();

            for (int y = 0; y < insts.numAttributes() - 1; y++)
            {
                dataGridView1.Rows[y].Cells[0].Value = insts.attribute(y).name();
                if (insts.attribute(y).isNominal())
                {
                    string phrase = insts.attribute(y).toString();
                    string[] first = phrase.Split('{');
                    string[] second = first[1].Split('}');
                    string[] attributeValues = second[0].Split(',');

                    DataGridViewComboBoxCell comboColumn = new DataGridViewComboBoxCell();

                    foreach (var a in attributeValues)
                    {
                        comboColumn.Items.Add(a);
                    }
                    dataGridView1.Rows[y].Cells[1] = comboColumn;
                }
            }

            // Veriyi işleyin
            weka.filters.unsupervised.attribute.Discretize myNominalData = new weka.filters.unsupervised.attribute.Discretize();
            myNominalData.setInputFormat(insts);
            insts = weka.filters.Filter.useFilter(insts, myNominalData);

            // Veri kümesindeki örneklerin sırasını rastgele karıştırın
            weka.filters.unsupervised.instance.Randomize myRandom = new weka.filters.unsupervised.instance.Randomize();
            myRandom.setInputFormat(insts);
            insts = weka.filters.Filter.useFilter(insts, myRandom);

            int trainSize = insts.numInstances() * percentSplit / 100;
            int testSize = insts.numInstances() - trainSize;
            weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);

            cl_Naive.buildClassifier(train);

            int numCorrect = 0;
            for (int i = trainSize; i < insts.numInstances(); i++)
            {
                weka.core.Instance currentInst = insts.instance(i);
                double predictedClass = cl_Naive.classifyInstance(currentInst);
                if (predictedClass == insts.instance(i).classValue())
                    numCorrect++;
            }

            res_Naive = (double)((double)numCorrect / (double)testSize * 100.0);

            successPercent.Add(res_Naive);

            //kNN 

            weka.core.Instances insts2 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
            insts2.setClassIndex(insts2.numAttributes() - 1);

            // 10-fold cross-validation for kNN
            weka.classifiers.lazy.IBk cl_Knn = new weka.classifiers.lazy.IBk();
            weka.classifiers.Evaluation eval2 = new weka.classifiers.Evaluation(insts2);
            eval2.crossValidateModel(cl_Knn, insts2, 10, new java.util.Random(1));

            res_KNN = eval2.pctCorrect();
            successPercent.Add(res_KNN);

            // Nominal to Binary
            weka.filters.unsupervised.attribute.NominalToBinary myBinaryData = new weka.filters.unsupervised.attribute.NominalToBinary();
            myBinaryData.setInputFormat(insts2);
            insts2 = weka.filters.Filter.useFilter(insts2, myBinaryData);

            // Normalization
            weka.filters.unsupervised.instance.Normalize myNormalized = new weka.filters.unsupervised.instance.Normalize();
            myNormalized.setInputFormat(insts2);
            insts2 = weka.filters.Filter.useFilter(insts2, myNormalized);

            // Randomize the order of the instances in the dataset
            weka.filters.unsupervised.instance.Randomize myRandom2 = new weka.filters.unsupervised.instance.Randomize();
            myRandom2.setInputFormat(insts2);
            insts2 = weka.filters.Filter.useFilter(insts2, myRandom2);

            int trainSize2 = insts2.numInstances() * percentSplit / 100;
            int testSize2 = insts2.numInstances() - trainSize2;
            weka.core.Instances train2 = new weka.core.Instances(insts2, 0, trainSize2);

            cl_Knn.buildClassifier(train2);

            int numCorrect2 = 0;
            for (int i = trainSize2; i < insts2.numInstances(); i++)
            {
                weka.core.Instance currentInst2 = insts2.instance(i);
                double predictedClass2 = cl_Knn.classifyInstance(currentInst2);
                if (predictedClass2 == insts2.instance(i).classValue())
                    numCorrect2++;
            }
            res_KNN = (double)((double)numCorrect2 / (double)testSize2 * 100.0);
            successPercent.Add(res_KNN);

            // Decision tree
            weka.core.Instances insts3 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
            insts3.setClassIndex(insts3.numAttributes() - 1);

            // 10-fold cross-validation for decision tree
            weka.classifiers.trees.J48 cl_Tree = new weka.classifiers.trees.J48();
            weka.classifiers.Evaluation eval3 = new weka.classifiers.Evaluation(insts3);
            eval3.crossValidateModel(cl_Tree, insts3, 10, new java.util.Random(1));

             res_Tree = eval3.pctCorrect();
            successPercent.Add(res_Tree);

            weka.filters.unsupervised.instance.Normalize myNormalized2 = new weka.filters.unsupervised.instance.Normalize();
            myNormalized2.setInputFormat(insts3);
            insts3 = weka.filters.Filter.useFilter(insts3, myNormalized2);

            weka.filters.unsupervised.instance.Randomize myRandom3 = new weka.filters.unsupervised.instance.Randomize();
            myRandom3.setInputFormat(insts3);
            insts3 = weka.filters.Filter.useFilter(insts3, myRandom3);

            int trainSize3 = insts3.numInstances() * percentSplit / 100;
            int testSize3 = insts3.numInstances() - trainSize3;
            weka.core.Instances train3 = new weka.core.Instances(insts3, 0, trainSize3);

            cl_Tree.buildClassifier(train3);

            int numCorrect3 = 0;
            for (int i = trainSize3; i < insts3.numInstances(); i++)
            {
                weka.core.Instance currentInst3 = insts3.instance(i);
                double predictedClass3 = cl_Tree.classifyInstance(currentInst3);
                if (predictedClass3 == insts3.instance(i).classValue())
                    numCorrect3++;
            }
            res_Tree = (double)((double)numCorrect3 / (double)testSize3 * 100.0);
            successPercent.Add(res_Tree);


            // Neural Network
            weka.core.Instances insts4 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
            insts4.setClassIndex(insts4.numAttributes() - 1);

            // 10-fold cross-validation for neural network
            weka.classifiers.functions.MultilayerPerceptron cl_NN = new weka.classifiers.functions.MultilayerPerceptron();
            weka.classifiers.Evaluation eval4 = new weka.classifiers.Evaluation(insts4);
            eval4.crossValidateModel(cl_NN, insts4, 10, new java.util.Random(1));

             res_NN = eval4.pctCorrect();
            successPercent.Add(res_NN);

            weka.filters.unsupervised.attribute.NominalToBinary myBinaryData2 = new weka.filters.unsupervised.attribute.NominalToBinary();
            myBinaryData2.setInputFormat(insts4);
            insts4 = weka.filters.Filter.useFilter(insts4, myBinaryData2);

            weka.filters.unsupervised.instance.Normalize myNormalized3 = new weka.filters.unsupervised.instance.Normalize();
            myNormalized3.setInputFormat(insts4);
            insts4 = weka.filters.Filter.useFilter(insts4, myNormalized3);

            weka.filters.unsupervised.instance.Randomize myRandom4 = new weka.filters.unsupervised.instance.Randomize();
            myRandom4.setInputFormat(insts4);
            insts4 = weka.filters.Filter.useFilter(insts4, myRandom4);

            int trainSize4 = insts4.numInstances() * percentSplit / 100;
            int testSize4 = insts4.numInstances() - trainSize4;
            weka.core.Instances train4 = new weka.core.Instances(insts4, 0, trainSize4);

            cl_NN.buildClassifier(train4);

            int numCorrect4 = 0;
            for (int i = trainSize4; i < insts4.numInstances(); i++)
            {
                weka.core.Instance currentInst4 = insts4.instance(i);
                double predictedClass4 = cl_NN.classifyInstance(currentInst4);
                if (predictedClass4 == insts4.instance(i).classValue())
                    numCorrect4++;
            }

            res_NN = (double)((double)numCorrect4 / (double)testSize4 * 100.0);
            successPercent.Add(res_NN);


            // SVM
            weka.core.Instances insts5 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
            insts5.setClassIndex(insts5.numAttributes() - 1);

            // 10-fold cross-validation for SVM
            weka.classifiers.functions.SMO cl_SVM = new weka.classifiers.functions.SMO();
            weka.classifiers.Evaluation eval5 = new weka.classifiers.Evaluation(insts5);
            eval5.crossValidateModel(cl_SVM, insts5, 10, new java.util.Random(1));

             res_SVM = eval5.pctCorrect();
            successPercent.Add(res_SVM);

            weka.filters.unsupervised.attribute.NominalToBinary myBinaryData3 = new weka.filters.unsupervised.attribute.NominalToBinary();
            myBinaryData3.setInputFormat(insts5);
            insts5 = weka.filters.Filter.useFilter(insts5, myBinaryData3);

            weka.filters.unsupervised.instance.Normalize myNormalized4 = new weka.filters.unsupervised.instance.Normalize();
            myNormalized4.setInputFormat(insts5);
            insts5 = weka.filters.Filter.useFilter(insts5, myNormalized4);

            weka.filters.unsupervised.instance.Randomize myRandom5 = new weka.filters.unsupervised.instance.Randomize();
            myRandom5.setInputFormat(insts5);
            insts5 = weka.filters.Filter.useFilter(insts5, myRandom5);

            int trainSize5 = insts5.numInstances() * percentSplit / 100;
            int testSize5 = insts5.numInstances() - trainSize5;
            weka.core.Instances train5 = new weka.core.Instances(insts5, 0, trainSize5);

            cl_SVM.buildClassifier(train5);

            int numCorrect5 = 0;
            for (int i = trainSize5; i < insts5.numInstances(); i++)
            {
                weka.core.Instance currentInst5 = insts5.instance(i);
                double predictedClass5 = cl_SVM.classifyInstance(currentInst5);
                if (predictedClass5 == insts5.instance(i).classValue())
                    numCorrect5++;
            }

            res_SVM = (double)((double)numCorrect5 / (double)testSize5 * 100.0);
            successPercent.Add(res_SVM);


            for (int i = 0; i < successPercent.Count; i++)
            {

                if ((double)successPercent[i] > max)
                {
                    max = (double)successPercent[i];
                    count = i + 1;
                }

            }
            for (int i = 0; i < Math.Min(count, algorithms.Count); i++)
            {
                nameOfAlgo = (string)algorithms[i];
            }

            textBox1.Text = nameOfAlgo + " is the most successful algorithm for this data set." + "(" + max + "%)\n";


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_Discover_Click(object sender, EventArgs e)
        {

            String s_newInstance = "";
            StreamReader sr = new StreamReader(fileDirectory);
            StreamWriter sw = new StreamWriter(@"test.arff", true);
            string line = "";
            string comp = "@data";
            string comp2 = "@DATA";
            line = sr.ReadLine();
            do
            {
                sw.WriteLine(line);
                if (line == comp || line == comp2)
                    break;
            } while ((line = sr.ReadLine()) != null);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null) // Check if the cell value is not null
                {
                    s_newInstance += (String)dataGridView1.Rows[i].Cells[1].Value + ",";
                }
                else
                {
                    // Handle empty cell or set default value if needed
                    s_newInstance += "?,";
                }
            }
            s_newInstance = s_newInstance.TrimEnd(','); 

            sw.WriteLine(s_newInstance);


            sr.Close();
            sw.Close();

            switch (count)
            {
                case 1:
                    weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(fileDirectory));
                    insts.setClassIndex(insts.numAttributes() - 1);

                    weka.filters.Filter myNominalData = new weka.filters.unsupervised.attribute.Discretize();
                    myNominalData.setInputFormat(insts);
                    insts = weka.filters.Filter.useFilter(insts, myNominalData);

                    // randomize the order of the instances in the dataset.
                    weka.filters.Filter myRandom = new weka.filters.unsupervised.instance.Randomize();
                    myRandom.setInputFormat(insts);
                    insts = weka.filters.Filter.useFilter(insts, myRandom);

                    // Define the input format for the classifier
                    cl_Naive.buildClassifier(insts);

                    double predictedClass = cl_Naive.classifyInstance(insts.instance(0));
                    textBox3.Text = insts.classAttribute().value(Convert.ToInt32(predictedClass));
                    break;

                case 2:
                    weka.core.Instances insts2 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
                    insts2.setClassIndex(insts2.numAttributes() - 1);

                    // Nominal to Binary
                    weka.filters.Filter myBinaryData = new weka.filters.unsupervised.attribute.NominalToBinary();
                    myBinaryData.setInputFormat(insts2);
                    insts2 = weka.filters.Filter.useFilter(insts2, myBinaryData);

                    // Normalization
                    weka.filters.Filter myNormalized = new weka.filters.unsupervised.instance.Normalize();
                    myNormalized.setInputFormat(insts2);
                    insts2 = weka.filters.Filter.useFilter(insts2, myNormalized);

                    // Randomize the order of the instances in the dataset.
                    weka.filters.Filter myRandom2 = new weka.filters.unsupervised.instance.Randomize();
                    myRandom2.setInputFormat(insts2);
                    insts2 = weka.filters.Filter.useFilter(insts2, myRandom2);

                    // Define the input format for the classifier
                    cl_Knn.buildClassifier(insts2);

                    double predictedClass2 = cl_Knn.classifyInstance(insts2.instance(0));
                    textBox3.Text = insts2.classAttribute().value(Convert.ToInt32(predictedClass2));
                    break;

                case 3:
                    weka.core.Instances insts3 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
                    insts3.setClassIndex(insts3.numAttributes() - 1);

                    // Normalization
                    weka.filters.Filter myNormalized2 = new weka.filters.unsupervised.instance.Normalize();
                    myNormalized2.setInputFormat(insts3);
                    insts3 = weka.filters.Filter.useFilter(insts3, myNormalized2);

                    // Randomize the order of the instances in the dataset.
                    weka.filters.Filter myRandom3 = new weka.filters.unsupervised.instance.Randomize();
                    myRandom3.setInputFormat(insts3);
                    insts3 = weka.filters.Filter.useFilter(insts3, myRandom3);

                    // Define the input format for the classifier
                    cl_Tree.buildClassifier(insts3);

                    double predictedClass3 = cl_Tree.classifyInstance(insts3.instance(0));
                    textBox3.Text = insts3.classAttribute().value(Convert.ToInt32(predictedClass3));
                    break;
                case 4:
                    weka.core.Instances insts4 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
                    insts4.setClassIndex(insts4.numAttributes() - 1);

                    // Dönüşümleri uygula
                    weka.filters.Filter myBinaryData2 = new weka.filters.unsupervised.attribute.NominalToBinary();
                    myBinaryData2.setInputFormat(insts4);
                    insts4 = weka.filters.Filter.useFilter(insts4, myBinaryData2);

                    weka.filters.Filter myNormalized3 = new weka.filters.unsupervised.instance.Normalize();
                    myNormalized3.setInputFormat(insts4);
                    insts4 = weka.filters.Filter.useFilter(insts4, myNormalized3);

                    weka.filters.Filter myRandom4 = new weka.filters.unsupervised.instance.Randomize();
                    myRandom4.setInputFormat(insts4);
                    insts4 = weka.filters.Filter.useFilter(insts4, myRandom4);

                    // cl_NN sınıflandırıcısına girdi veri formatını tanımla
                    cl_NN = new weka.classifiers.functions.MultilayerPerceptron();
                    cl_NN.buildClassifier(insts4);

                    double predictedClass4 = cl_NN.classifyInstance(insts4.instance(0));
                    textBox3.Text = insts4.classAttribute().value(Convert.ToInt32(predictedClass4));

                    break;
                case 5:
                    weka.core.Instances insts5 = new weka.core.Instances(new java.io.FileReader(fileDirectory));
                    insts5.setClassIndex(insts5.numAttributes() - 1);

                    // Define the classifier
                    cl_SVM = new weka.classifiers.functions.SMO();

                    // Nominal to Binary
                    weka.filters.Filter myBinaryData3 = new weka.filters.unsupervised.attribute.NominalToBinary();
                    myBinaryData3.setInputFormat(insts5);
                    insts5 = weka.filters.Filter.useFilter(insts5, myBinaryData3);

                    // Normalization
                    weka.filters.Filter myNormalized4 = new weka.filters.unsupervised.instance.Normalize();
                    myNormalized4.setInputFormat(insts5);
                    insts5 = weka.filters.Filter.useFilter(insts5, myNormalized4);

                    // Randomize the order of the instances in the dataset.
                    weka.filters.Filter myRandom5 = new weka.filters.unsupervised.instance.Randomize();
                    myRandom5.setInputFormat(insts5);
                    insts5 = weka.filters.Filter.useFilter(insts5, myRandom5);

                    // Define the input format for the classifier
                    cl_SVM.buildClassifier(insts5);

                    double predictedClass5 = cl_SVM.classifyInstance(insts5.instance(0));
                    textBox3.Text = insts5.classAttribute().value(Convert.ToInt32(predictedClass5));
                    break;

                default:
                    textBox3.Text = "Error!";
                    break;


            }



        }
    }
}
